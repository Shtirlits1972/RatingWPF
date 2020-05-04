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
            List<ScaleLine> scaleLines = GetScaleLines(list, Dtt);

            PaintScale3(scaleLines, list[0].emitent_name_rus);

        }

        private void PaintScale3(List<ScaleLine> scaleLines, string emitent_name_rus)
        {
            Grid gridMain = new Grid();
            #region Head
            ColumnDefinition emitentColumnDef = new ColumnDefinition();
            emitentColumnDef.Width = new GridLength(300);

            gridMain.ColumnDefinitions.Add(emitentColumnDef);

            ColumnDefinition nationalColumnDef = new ColumnDefinition();
            nationalColumnDef.Width = new GridLength(600);

            gridMain.ColumnDefinitions.Add(nationalColumnDef);


            ColumnDefinition foreignColumnDef = new ColumnDefinition();
            foreignColumnDef.Width = new GridLength(600);

            gridMain.ColumnDefinitions.Add(foreignColumnDef);

            RowDefinition rowHead = new RowDefinition();
            rowHead.Height = new GridLength(30);

            TextBlock txtHeadMark = new TextBlock();
            txtHeadMark.Text = emitent_name_rus;
            txtHeadMark.HorizontalAlignment = HorizontalAlignment.Center;
            txtHeadMark.VerticalAlignment = VerticalAlignment.Bottom;
            txtHeadMark.FontSize = 20;
            txtHeadMark.FontWeight = FontWeight.FromOpenTypeWeight(700); 

            Grid.SetRow(txtHeadMark, 0);
            Grid.SetColumn(txtHeadMark, 0);
            gridMain.Children.Add(txtHeadMark);


            gridMain.RowDefinitions.Add(rowHead);

            TextBlock txtNational = new TextBlock();
            txtNational.Text = "Национальная";
            txtNational.Margin = new Thickness(0, 0, 0, 0);
            txtNational.FontSize = 20;
            txtNational.FontWeight = FontWeight.FromOpenTypeWeight(700);

            DockPanel dockPanelN = new DockPanel();
            dockPanelN.HorizontalAlignment = HorizontalAlignment.Center;
            dockPanelN.VerticalAlignment = VerticalAlignment.Bottom;
            dockPanelN.Children.Add(txtNational);

            Grid.SetRow(dockPanelN, 0);
            Grid.SetColumn(dockPanelN, 1);

            gridMain.Children.Add(dockPanelN);


            TextBlock txtForeign = new TextBlock();
            txtForeign.Text = "Международная";
            txtForeign.Margin = new Thickness(10, 0, 0, 0);
            txtForeign.FontSize = 20;
            txtForeign.FontWeight = FontWeight.FromOpenTypeWeight(700);

            DockPanel dockPanelF = new DockPanel();
            dockPanelF.HorizontalAlignment = HorizontalAlignment.Center;
            dockPanelF.VerticalAlignment = VerticalAlignment.Bottom;
            dockPanelF.Children.Add(txtForeign);

            Grid.SetRow(dockPanelF, 0);
            Grid.SetColumn(dockPanelF, 2);


            gridMain.Children.Add(dockPanelF);
            #endregion

            for(int i=0; i< scaleLines.Count; i++)
            {
                RowDefinition rowItem = new RowDefinition();
                rowItem.Height = new GridLength(180);
                gridMain.RowDefinitions.Add(rowItem);

                #region Head
                Grid gridHead = new Grid();
                gridHead.Background = new SolidColorBrush(Colors.LightCyan);
                gridHead.Margin = new Thickness(1, 1, 1, 1);

                Grid.SetRow(gridHead, i + 1);
                Grid.SetColumn(gridHead, 0);

                RowDefinition rowAgency = new RowDefinition();
                rowAgency.Height = new GridLength(25);
                gridHead.RowDefinitions.Add(rowAgency);

                TextBlock textAgency = new TextBlock();
                textAgency.Text = scaleLines[i].AgencyName;
                textAgency.FontWeight = FontWeight.FromOpenTypeWeight(700);
                textAgency.FontSize = 20;  
                textAgency.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textAgency, 0);
                Grid.SetColumn(textAgency, 0);
                gridHead.Children.Add(textAgency);

                RowDefinition rowStatusN = new RowDefinition();
                rowStatusN.Height = new GridLength(25);
                gridHead.RowDefinitions.Add(rowStatusN);

                TextBlock textStatusN = new TextBlock();
                textStatusN.Text = "Национальный статус: " +  scaleLines[i].StatusN;
                textStatusN.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textStatusN, 1);
                Grid.SetColumn(textStatusN, 0);
                gridHead.Children.Add(textStatusN);
                //-----------------------------------------
                RowDefinition rowDataN = new RowDefinition();
                rowDataN.Height = new GridLength(25);
                gridHead.RowDefinitions.Add(rowDataN);

                TextBlock textDataN = new TextBlock();

                string strDataN = "";

                if(scaleLines[i].DateN != null)
                {
                    if (!String.IsNullOrEmpty(((DateTime)scaleLines[i].DateN).ToShortDateString()))
                    {
                        strDataN = ((DateTime)scaleLines[i].DateN).ToShortDateString();
                    }
                }

                textDataN.Text = "Дата: " + strDataN;

                textDataN.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textDataN, 2);
                Grid.SetColumn(textDataN, 0);
                gridHead.Children.Add(textDataN);
                //-------------------------

                RowDefinition rowStatusF = new RowDefinition();
                rowStatusF.Height = new GridLength(25);
                gridHead.RowDefinitions.Add(rowStatusF);

                TextBlock textStatusF = new TextBlock();
                textStatusF.Text = "Международный статус: " + scaleLines[i].StatusF;
                textStatusF.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textStatusF, 3);
                Grid.SetColumn(textStatusF, 0);
                gridHead.Children.Add(textStatusF);

                //-----------------------------------------
                RowDefinition rowDataF = new RowDefinition();
                rowDataF.Height = new GridLength(25);
                gridHead.RowDefinitions.Add(rowDataF);

                TextBlock textDataF = new TextBlock();

                string strDataF = "";

                if (scaleLines[i].DateF != null)
                {
                    if (!String.IsNullOrEmpty(((DateTime)scaleLines[i].DateF).ToShortDateString()))
                    {
                        strDataF = ((DateTime)scaleLines[i].DateF).ToShortDateString();
                    }
                }

                textDataF.Text = "Дата: " + strDataF;

                textDataF.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textDataF, 4);
                Grid.SetColumn(textDataF, 0);
                gridHead.Children.Add(textDataF);
                //-------------------------

                gridMain.Children.Add(gridHead); 
                #endregion

                #region National
                Grid gridNational = new Grid();
                gridNational.Background = new SolidColorBrush(Colors.LightCyan);
                gridNational.Margin = new Thickness(1, 1, 1, 1);

                Grid.SetRow(gridNational, i + 1);
                Grid.SetColumn(gridNational, 1);

                if(scaleLines[i].National != null)
                {
                    gridNational = GetGridScale(gridNational, scaleLines[i].National, scaleLines[i].DateN, scaleLines[i].PointNameN);
                }

                gridMain.Children.Add(gridNational);
                #endregion

                #region Foreign
                Grid gridForeign = new Grid();
                gridForeign.Background = new SolidColorBrush(Colors.LightCyan);
                gridForeign.Margin = new Thickness(1, 1, 1, 1);

                Grid.SetRow(gridForeign, i + 1);
                Grid.SetColumn(gridForeign, 2);

                if (scaleLines[i].Foreign != null)
                {
                   gridForeign = GetGridScale(gridForeign, scaleLines[i].Foreign, scaleLines[i].DateF, scaleLines[i].PointNameF);
                }

                gridMain.Children.Add(gridForeign); 
                #endregion
            }

            ScalePanel.Children.Add(gridMain);
        }

        private Grid GetGridScale(Grid grid, List<ScalePart> scaleParts, DateTime? date,  string PointName="")
        {
            RowDefinition row0 = new RowDefinition();
            row0.Height = new GridLength(30);
            grid.RowDefinitions.Add(row0);

            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            grid.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(70);
            grid.RowDefinitions.Add(row2);

            RowDefinition rowdata = new RowDefinition();
            rowdata.Height = new GridLength(30);
            grid.RowDefinitions.Add(rowdata);

            TextBlock textBlockData = new TextBlock();

            string strData = "";

            if(date != null)
            {
                strData = ((DateTime)date).ToShortDateString();
            }

            textBlockData.Text = strData;
            textBlockData.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetRow(textBlockData, 3);
            Grid.SetColumn(textBlockData, 0);
            Grid.SetColumnSpan(textBlockData, scaleParts.Count);

            grid.Children.Add(textBlockData);


            for (int j = 0; j < scaleParts.Count; j++)
            {
                #region row II
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(20);

                grid.ColumnDefinitions.Add(columnDefinition);

                TextBlock block = new TextBlock();
                string name = scaleParts[j].rating_scale_point_name;
                block.Text = scaleParts[j].rating_scale_point_name;

                RotateTransform rotate = new RotateTransform(-90);
                block.LayoutTransform = rotate;

                TextBlock txtTooltip = new TextBlock();
                txtTooltip.Width = 250;

                int HeightBlock = (scaleParts[j].rating_scale_point_description_rus.Length / 25) * 20;

                if(HeightBlock == 0)
                {
                    HeightBlock = 20;
                }

                txtTooltip.Height = HeightBlock;
                txtTooltip.TextWrapping = TextWrapping.WrapWithOverflow;
                txtTooltip.TextAlignment = TextAlignment.Left;
                txtTooltip.Text = scaleParts[j].rating_scale_point_description_rus;

                block.ToolTip = txtTooltip;
                block.Margin = new Thickness(2, 2, 2, 2);
                block.Background = new SolidColorBrush(Colors.LightGray);
                block.TextAlignment = TextAlignment.Center;
                block.FontSize = 10;

                Grid.SetRow(block, 2);
                Grid.SetColumn(block, j);

                grid.Children.Add(block);
                #endregion

                #region TextBlock
                TextBlock block1 = new TextBlock();
                if (ScalePartCrud.GetGraceWorld().Contains(name))
                {
                    block1.Background = new SolidColorBrush(Colors.Gray);
                }
                else if (j <= 3)
                {
                    block1.Background = new SolidColorBrush(Colors.Green);
                }
                else if ((j > 3) && (j < 11))
                {
                    block1.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if ((j >= 11))
                {
                    block1.Background = new SolidColorBrush(Colors.Red);
                }

                block1.Margin = new Thickness(1, 1, 1, 1);

                Grid.SetRow(block1, 1);
                Grid.SetColumn(block1, j);
                grid.Children.Add(block1);
                #endregion

                #region Triangle

                if (!String.IsNullOrEmpty(PointName))
                {
                    if (scaleParts[j].rating_scale_point_name.Trim() == PointName.Trim())
                    {
                        Canvas canvas = new Canvas();
                        Grid.SetRow(canvas, 0);
                        Grid.SetColumn(canvas, j);

                        Polygon myPolygon = new Polygon();
                        myPolygon.Stroke = Brushes.Black;
                        myPolygon.Fill = Brushes.Blue;
                        myPolygon.StrokeThickness = 1;
                        myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
                        myPolygon.VerticalAlignment = VerticalAlignment.Center;
                        Point Point1 = new Point(0, 15);
                        Point Point2 = new Point(10, 30);
                        Point Point3 = new Point(20, 15);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(Point1);
                        myPointCollection.Add(Point2);
                        myPointCollection.Add(Point3);
                        myPolygon.Points = myPointCollection;
                        canvas.Children.Add(myPolygon);

                        grid.Children.Add(canvas);
                    }
                }
                #endregion
            }
            return grid;
        }
        private List<ScaleLine> GetScaleLines(List<Rating> list, DateTime Dtt)
        {
            List<ScaleLine> scaleLines = new List<ScaleLine>();
            List<string> emitents = list.Select(s => s.agency_name_rus).Distinct().ToList();

            for(int i=0; i< emitents.Count; i++)
            {
                ScaleLine scaleLine = new ScaleLine { AgencyName = emitents[i] };

                for(int j=0; j< list.Count; j++)
                {
                    if(scaleLine.AgencyName == list[j].agency_name_rus)
                    {
                        List<ScalePart> scaleParts = ScalePartCrud.GetAllScalePart(Dtt, list[j].agency_id, list[j].scale_id);

                        if(scaleParts[0].typeF == "N" && (scaleLine.National == null))
                        {
                            scaleLine.PointNameN = list[j].point_name;
                            scaleLine.StatusN = list[j].name_rus;
                            scaleLine.National = scaleParts;

                            if(list[j].rating_date != null)
                            {
                                scaleLine.DateN = list[j].rating_date;
                            } 
                        }
                        else if (scaleParts[0].typeF == "F" && (scaleLine.Foreign == null))
                        {
                            scaleLine.PointNameF = list[j].point_name;
                            scaleLine.StatusF = list[j].name_rus;
                            scaleLine.Foreign = scaleParts;

                            if (list[j].rating_date != null)
                            {
                                scaleLine.DateF = list[j].rating_date;
                            }
                        }
                    }
                }

                scaleLines.Add(scaleLine);
            }

            return scaleLines;
        }
        private void PaintScale(List<Rating> list, DateTime Dtt)
        {
                Grid gridMain = new Grid();

                ColumnDefinition leftColumnDef = new ColumnDefinition();
                leftColumnDef.Width = new GridLength(450);

                gridMain.ColumnDefinitions.Add(leftColumnDef);

                ColumnDefinition rightColumnDef = new ColumnDefinition();
                rightColumnDef.Width = new GridLength(10500);

                gridMain.ColumnDefinitions.Add(rightColumnDef);

                for (int j = 0; j < list.Count; j++)
                {
                    RowDefinition rowM = new RowDefinition();
                    rowM.Height = new GridLength(150);
                    gridMain.RowDefinitions.Add(rowM);


                    Grid gridRight = new Grid();
                    gridRight.Background = new SolidColorBrush(Colors.LightCyan);
                    gridRight.Margin = new Thickness(2, 2, 2, 2);

                    List<ScalePart> listScaleParts = ScalePartCrud.GetAllScalePart(Dtt, list[j].agency_id, list[j].scale_id);

                    string Emitent = listScaleParts[0].name_of_emitent_rus;

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
                    string typeScale = "F";

                    for (int i = 0; i < listScaleParts.Count; i++)
                    {
                        if (i==0)
                        {
                            rating_scale_name_rus = listScaleParts[i].rating_scale_name_rus;
                            rating_scale_name_eng = listScaleParts[i].rating_scale_name_eng;
                            name_of_emitent_rus = listScaleParts[i].name_of_emitent_rus;
                            name_of_emitent_eng = listScaleParts[i].name_of_emitent_eng;
                            dt_act = listScaleParts[i].dt_act.ToString("D");
                            typeScale = listScaleParts[i].typeF;
                        }

                        #region row II
                        ColumnDefinition columnDefinition = new ColumnDefinition();
                        columnDefinition.Width = new GridLength(20);

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
                     //   block.FontWeight = FontWeight.FromOpenTypeWeight(500);
                        block.FontSize = 10;

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
                        else if ((i > 3) && (i < 11))
                        {
                            block1.Background = new SolidColorBrush(Colors.Yellow);
                        }
                        else if ((i >= 11))
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
                            System.Windows.Point Point1 = new System.Windows.Point(0, 15);
                            System.Windows.Point Point2 = new System.Windows.Point(10, 30);
                            System.Windows.Point Point3 = new System.Windows.Point(20, 15);
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
                    //RowDefinition rowL1 = new RowDefinition();
                    //rowL1.Height = new GridLength(25);
                    //gridLeft.RowDefinitions.Add(rowL1);

                    //TextBlock textBlockL1 = new TextBlock();
                    //textBlockL1.Text = rating_scale_name_rus;
                    //textBlockL1.Margin = new Thickness(5, 0, 0, 1);
                    //Grid.SetRow(textBlockL1, 0);
                    //Grid.SetColumn(textBlockL1, 0);
                    //gridLeft.Children.Add(textBlockL1);
                    ////---------------------------------------------
                    //RowDefinition rowL2 = new RowDefinition();
                    //rowL2.Height = new GridLength(25);
                    //gridLeft.RowDefinitions.Add(rowL2);

                    //TextBlock textBlockL2 = new TextBlock();
                    //textBlockL2.Text = rating_scale_name_eng;
                    //textBlockL2.Margin = new Thickness(5, 0, 0, 1);
                    //Grid.SetRow(textBlockL2, 1);
                    //Grid.SetColumn(textBlockL2, 0);
                    //gridLeft.Children.Add(textBlockL2);
                    ////---------------------------------------------
                    RowDefinition rowL3 = new RowDefinition();
                    rowL3.Height = new GridLength(25);
                    gridLeft.RowDefinitions.Add(rowL3);

                    TextBlock textBlockL3 = new TextBlock();
                    textBlockL3.Text = name_of_emitent_rus;
                    textBlockL3.Margin = new Thickness(5, 0, 0, 1);
                    Grid.SetRow(textBlockL3, 0);
                    Grid.SetColumn(textBlockL3, 0);
                    gridLeft.Children.Add(textBlockL3);
                    //---------------------------------------------
                    //RowDefinition rowL4 = new RowDefinition();
                    //rowL4.Height = new GridLength(25);
                    //gridLeft.RowDefinitions.Add(rowL4);

                    //TextBlock textBlockL4 = new TextBlock();
                    //textBlockL4.Text = name_of_emitent_eng;
                    //textBlockL4.Margin = new Thickness(5, 0, 0, 1);
                    //Grid.SetRow(textBlockL4, 3);
                    //Grid.SetColumn(textBlockL4, 0);
                    //gridLeft.Children.Add(textBlockL4);
                    ////---------------------------------------------
                    //RowDefinition rowL5 = new RowDefinition();
                    //rowL5.Height = new GridLength(25);
                    //gridLeft.RowDefinitions.Add(rowL5);

                    //TextBlock textBlockL5 = new TextBlock();
                    //textBlockL5.Text = dt_act;
                    //textBlockL5.Margin = new Thickness(5, 0, 0, 1);
                    //Grid.SetRow(textBlockL5, 4);
                    //Grid.SetColumn(textBlockL5, 0);
                    //gridLeft.Children.Add(textBlockL5);
                //--------------------------------------------------------
                RowDefinition rowL6 = new RowDefinition();
                rowL6.Height = new GridLength(25);
                gridLeft.RowDefinitions.Add(rowL6);

                TextBlock textBlockL6 = new TextBlock();
                textBlockL6.Text = list[j].name_rus;
                textBlockL6.Margin = new Thickness(5, 0, 0, 1);
                Grid.SetRow(textBlockL6, 1);
                Grid.SetColumn(textBlockL6, 0);
                gridLeft.Children.Add(textBlockL6);
                //--------------------------------------------------------

                Grid.SetRow(gridLeft, j);
                    Grid.SetColumn(gridLeft, 0);
                    gridMain.Children.Add(gridLeft);
                }

                ScalePanel.Children.Add(gridMain);
            }
        }
    }

