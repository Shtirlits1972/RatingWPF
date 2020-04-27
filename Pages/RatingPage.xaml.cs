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
            int typeNum = 1;

            if ((bool)typeNum1.IsChecked == true)
            {
                typeNum = 1;
            }
            else if ((bool)typeNum1.IsChecked == false)
            {
                typeNum = 2;
            }

            List<Rating> list = RatingCrud.GetAll(strInn, Dtt, typeNum);

            ScalePanel.Children.Clear();
            PaintScale(list);

        }

        private void PaintScale(List<Rating> list)
        {
                Grid gridMain = new Grid();

                ColumnDefinition leftColumnDef = new ColumnDefinition();
                leftColumnDef.Width = new GridLength(300);

                gridMain.ColumnDefinitions.Add(leftColumnDef);

                ColumnDefinition rightColumnDef = new ColumnDefinition();
                rightColumnDef.Width = new GridLength(700);

                gridMain.ColumnDefinitions.Add(rightColumnDef);

                for (int j = 0; j < list.Count; j++)
                {
                    RowDefinition rowM = new RowDefinition();
                    rowM.Height = new GridLength(150);
                    gridMain.RowDefinitions.Add(rowM);


                    Grid gridRight = new Grid();
                    gridRight.Background = new SolidColorBrush(Colors.LightCyan);
                    gridRight.Margin = new Thickness(2, 2, 2, 2);

                    List<ScalePart> listScaleParts = ScalePartCrud.GetAllScalePart(list[j].rating_date, list[j].agency_id, list[j].scale_id);

                    RowDefinition row0 = new RowDefinition();
                    row0.Height = new GridLength(30);
                    gridRight.RowDefinitions.Add(row0);

                    RowDefinition row1 = new RowDefinition();
                    row1.Height = new GridLength(50);
                    gridRight.RowDefinitions.Add(row1);

                    RowDefinition row2 = new RowDefinition();
                    row2.Height = new GridLength(70);
                    gridRight.RowDefinitions.Add(row2);

                    string rating_scale_name_rus = "";
                    string rating_scale_name_eng = "";
                    string name_of_emitent_rus = "";
                    string name_of_emitent_eng = "";
                    string dt_act = "";

                    for (int i = 0; i < listScaleParts.Count; i++)
                    {
                        if (i==0)
                        {
                            rating_scale_name_rus = listScaleParts[i].rating_scale_name_rus;
                            rating_scale_name_eng = listScaleParts[i].rating_scale_name_eng;
                            name_of_emitent_rus = listScaleParts[i].name_of_emitent_rus;
                            name_of_emitent_eng = listScaleParts[i].name_of_emitent_eng;
                            dt_act = listScaleParts[i].dt_act.ToString("D");
                        }

                        #region row II
                        ColumnDefinition columnDefinition = new ColumnDefinition();
                        columnDefinition.Width = new GridLength(30);

                        gridRight.ColumnDefinitions.Add(columnDefinition);

                        TextBlock block = new TextBlock();
                        string name = listScaleParts[i].rating_scale_point_name;
                        block.Text = listScaleParts[i].rating_scale_point_name;

                        RotateTransform rotate = new RotateTransform(-90);
                        block.LayoutTransform = rotate;

                        block.ToolTip = listScaleParts[i].rating_scale_point_description_rus;
                        block.Margin = new Thickness(2, 2, 2, 2);
                        block.Background = new SolidColorBrush(Colors.LightGray);
                        block.TextAlignment = TextAlignment.Center;
                        block.FontWeight = FontWeight.FromOpenTypeWeight(500);
                        block.FontSize = 14;

                        Grid.SetRow(block, 2);
                        Grid.SetColumn(block, i);

                        gridRight.Children.Add(block);
                        #endregion

                        #region TextBlock
                        TextBlock block1 = new TextBlock();
                        if (ScalePartCrud.GetGraceWorld().Contains(name))
                        {
                            block1.Background = new SolidColorBrush(Colors.Gray);
                        }
                        else if (i <= 3)
                        {
                            block1.Background = new SolidColorBrush(Colors.Green);
                        }
                        else if ((i > 3) && (i < 9))
                        {
                            block1.Background = new SolidColorBrush(Colors.Yellow);
                        }
                        else if ((i >= 9))
                        {
                            block1.Background = new SolidColorBrush(Colors.Red);
                        }

                        block1.Margin = new Thickness(1, 1, 1, 1);

                        Grid.SetRow(block1, 1);
                        Grid.SetColumn(block1, i);
                        gridRight.Children.Add(block1);
                        #endregion

                        #region Triangle
                        if (listScaleParts[i].rating_scale_point_name.Trim() == list[j].point_name.Trim())
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

                            gridRight.Children.Add(canvas);
                        } 
                        #endregion

                    }

                    Grid.SetRow(gridRight, j);
                    Grid.SetColumn(gridRight, 1);
                    gridMain.Children.Add(gridRight);

                    //=========================================================================================================
                    Grid gridLeft = new Grid();
                    gridLeft.Background = new SolidColorBrush(Colors.LightCyan);
                    gridLeft.Margin = new Thickness(2);
                    //---------------------------------------------
                    RowDefinition rowL1 = new RowDefinition();
                    rowL1.Height = new GridLength(30);
                    gridLeft.RowDefinitions.Add(rowL1);

                    TextBlock textBlockL1 = new TextBlock();
                    textBlockL1.Text = rating_scale_name_rus;
                    textBlockL1.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL1, 0);
                    Grid.SetColumn(textBlockL1, 0);
                    gridLeft.Children.Add(textBlockL1);
                    //---------------------------------------------
                    RowDefinition rowL2 = new RowDefinition();
                    rowL2.Height = new GridLength(30);
                    gridLeft.RowDefinitions.Add(rowL2);

                    TextBlock textBlockL2 = new TextBlock();
                    textBlockL2.Text = rating_scale_name_eng;
                    textBlockL2.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL2, 1);
                    Grid.SetColumn(textBlockL2, 0);
                    gridLeft.Children.Add(textBlockL2);
                    //---------------------------------------------
                    RowDefinition rowL3 = new RowDefinition();
                    rowL3.Height = new GridLength(30);
                    gridLeft.RowDefinitions.Add(rowL3);

                    TextBlock textBlockL3 = new TextBlock();
                    textBlockL3.Text = name_of_emitent_rus;
                    textBlockL3.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL3, 2);
                    Grid.SetColumn(textBlockL3, 0);
                    gridLeft.Children.Add(textBlockL3);
                    //---------------------------------------------
                    RowDefinition rowL4 = new RowDefinition();
                    rowL4.Height = new GridLength(30);
                    gridLeft.RowDefinitions.Add(rowL4);

                    TextBlock textBlockL4 = new TextBlock();
                    textBlockL4.Text = name_of_emitent_eng;
                    textBlockL4.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL4, 3);
                    Grid.SetColumn(textBlockL4, 0);
                    gridLeft.Children.Add(textBlockL4);
                    //---------------------------------------------
                    RowDefinition rowL5 = new RowDefinition();
                    rowL5.Height = new GridLength(30);
                    gridLeft.RowDefinitions.Add(rowL5);

                    TextBlock textBlockL5 = new TextBlock();
                    textBlockL5.Text = dt_act;
                    textBlockL5.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL5, 4);
                    Grid.SetColumn(textBlockL5, 0);
                    gridLeft.Children.Add(textBlockL5);
                    //--------------------------------------------------------

                    Grid.SetRow(gridLeft, j);
                    Grid.SetColumn(gridLeft, 0);
                    gridMain.Children.Add(gridLeft);
                }

                ScalePanel.Children.Add(gridMain);
            }
        }
    }

