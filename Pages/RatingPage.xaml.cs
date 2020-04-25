using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RatingWPF.Models;

namespace RatingWPF.Pages
{

    public partial class RatingPage : Page
    {
        public RatingPage()
        {
            InitializeComponent();

            DateTime dateTime = new DateTime(2020, 4, 21);
            picData.SelectedDate = dateTime;
            picData.DisplayDate = dateTime;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string strInn = txtINN.Text;
            DateTime Dtt = (DateTime)picData.SelectedDate;

            List<Rating> list = RatingCrud.GetAll(strInn, Dtt);

            gridMain.ItemsSource = list;
        }

        private void gridMain_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Rating item = (Rating)gridMain.SelectedItem;

            int PointId = item.rating_scale_point_id;

            string PointName = ScalePartCrud.GetPointName(PointId);

            List<ScalePart> listScaleParts = ScalePartCrud.GetAllScalePart(item.rating_date, item.agency_id, item.scale_id);

            if(listScaleParts != null && listScaleParts.Count > 0)
            {
                try
                {
                    PaintScale(listScaleParts, PointName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            int g = 0;
        }

        private void PaintScale(List<ScalePart> listScaleParts, string PointName)
        {
            ScalePanel.Children.Clear();
            Grid gridScale = new Grid();

            RowDefinition row0 = new RowDefinition();
            row0.Height = new GridLength(30);
            gridScale.RowDefinitions.Add(row0);

            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            gridScale.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(50);
            gridScale.RowDefinitions.Add(row2);

            for (int i = 0; i < listScaleParts.Count; i++)
            {
                #region row II
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(30);

                gridScale.ColumnDefinitions.Add(columnDefinition);

                TextBlock block = new TextBlock();
                block.Text = listScaleParts[i].rating_scale_point_name;

                RotateTransform rotate = new RotateTransform(-90);
                block.LayoutTransform = rotate;

                block.ToolTip = listScaleParts[i].rating_scale_point_description_rus;
                block.Margin = new Thickness(2,2,2,2);
                block.Background = new SolidColorBrush(Colors.LightGray);
                block.TextAlignment = TextAlignment.Center;

                Grid.SetRow(block, 2);
                Grid.SetColumn(block, i);

                gridScale.Children.Add(block);
                #endregion

                TextBlock block1 = new TextBlock();
                block1.Background = new SolidColorBrush(Colors.Green);
                block1.Margin = new Thickness(1,1,1,1);

                Grid.SetRow(block1, 1);
                Grid.SetColumn(block1, i);
                gridScale.Children.Add(block1);

                if(listScaleParts[i].rating_scale_point_name.Trim() == PointName.Trim())
                {
                    Canvas canvas = new Canvas();
                    Grid.SetRow(canvas, 0);
                    Grid.SetColumn(canvas, i);

                    Polygon myPolygon = new Polygon();
                    myPolygon.Stroke = System.Windows.Media.Brushes.Black;
                    myPolygon.Fill = System.Windows.Media.Brushes.Blue;
                    myPolygon.StrokeThickness = 1;
                    myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
                    myPolygon.VerticalAlignment = VerticalAlignment.Center;
                    System.Windows.Point Point1 = new System.Windows.Point(5, 15);
                    System.Windows.Point Point2 = new System.Windows.Point(15, 30);
                    System.Windows.Point Point3 = new System.Windows.Point(25, 15);
                    PointCollection myPointCollection = new PointCollection();
                    myPointCollection.Add(Point1);
                    myPointCollection.Add(Point2);
                    myPointCollection.Add(Point3);
                    myPolygon.Points = myPointCollection;
                    canvas.Children.Add(myPolygon);

                    gridScale.Children.Add(canvas);
                }

            }

            ScalePanel.Children.Add(gridScale);
        }
    }
}
