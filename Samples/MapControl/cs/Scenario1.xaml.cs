//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SDKTemplate;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using System;
using System.Globalization;
using System.Linq;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapControlSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Scenario1 : Page
    {
        private MainPage rootPage;

        public Scenario1()
        {
            this.InitializeComponent();
            myMap.Loaded += MyMap_Loaded;
            myMap.MapTapped += MyMap_MapTapped;
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center =
                new Geopoint(new BasicGeoposition()
                {
                    //Geopoint for Seattle 
                    Latitude = 47.604,
                    Longitude = -122.329
                });
            myMap.ZoomLevel = 12;

        }

        private void MyMap_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {
            var tappedGeoPosition = args.Location.Position;
            string status = "MapTapped at \nLatitude:" + tappedGeoPosition.Latitude + "\nLongitude: " + tappedGeoPosition.Longitude;
            rootPage.NotifyUser(status, NotifyType.StatusMessage);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;
        }

        private void TrafficFlowVisible_Checked(object sender, RoutedEventArgs e)
        {
            myMap.TrafficFlowVisible = true;
        }

        private void trafficFlowVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            myMap.TrafficFlowVisible = false;
        }



        private void styleCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (styleCombobox.SelectedIndex)
            {
                case 0:
                    myMap.Style = MapStyle.None;
                    break;
                case 1:
                    myMap.Style = MapStyle.Road;
                    break;
                case 2:
                    myMap.Style = MapStyle.Aerial;
                    break;
                case 3:
                    myMap.Style = MapStyle.AerialWithRoads;
                    break;
                case 4:
                    myMap.Style = MapStyle.Terrain;
                    break;
            }
        }

        /* Circle-like polygon (50 or 51 points)
            Center 26.7717155814171,23.8236242160201
Radius (m) 9002647.96
        */
        private const string PolygonDataBad =
            @"67.0069165388631,-175.687315100804
58.4739252714875,137.369805547722
41.1725039794677,114.618053849915
21.5846874738109,101.595665379442
1.44768971339321,91.3771990929829
-18.5025900985027,80.8815177556115
-37.43058191274,67.0616805883142
-53.3094081095494,44.0625177372409
-60.4613689658655,4.31268489919605
-53.3094081095494,-35.4371479388489
-37.43058191274,-58.4363107899222
-18.5025900985027,-72.2561479572194
1.44768971339319,-82.7518292945909
21.5846874738109,-92.9702955810504
41.1725039794677,-105.992684051523
58.4739252714875,-128.74443574933";

        /*Center 18.9950475934893,12.197297969833
Radius (m) 9002647.96
         */
        private const string PolygonDataGood = @"80.1327898063564,-167.802702030167
77.9171824366615,155.957467790097
72.9164979387876,135.49386705969
66.8747889267421,124.460979871086
60.4273718482956,117.664221542018
53.7952124646723,112.929069342179
47.069460156249,109.303996827713
40.2944833599294,106.320861110994
33.4952557169926,103.722495531965
26.6879142745222,101.352772788377
19.8844535888493,99.1072063594229
13.0951348530734,96.9083347724777
6.32995702946791,94.6920447130444
-0.400219661283769,92.3988823649588
-7.08257677675639,89.9674711359788
-13.7010240322357,87.3283850115727
-20.2344030373735,84.3972714902162
-26.6538353802256,81.0660836715321
-32.9186315749379,77.1911966385062
-38.96982835589,72.5773199801583
-44.7199427675184,66.9575854516631
-50.0372256659806,59.9763119728415
-54.7240306887938,51.1999530920365
-58.4965552230304,40.2244235145965
-60.9949723248601,26.9831155863162
-61.877115006665,12.197297969833
-60.9949723248601,-2.58851964665019
-58.4965552230304,-15.8298275749305
-54.7240306887938,-26.8053571523705
-50.0372256659806,-35.5817160331755
-44.7199427675184,-42.562989511997
-38.96982835589,-48.1827240404923
-32.9186315749379,-52.7966006988401
-26.6538353802256,-56.6714877318661
-20.2344030373734,-60.0026755505502
-13.7010240322357,-62.9337890719067
-7.08257677675639,-65.5728751963128
-0.400219661283758,-68.0042864252927
6.32995702946793,-70.2974487733784
13.0951348530734,-72.5137388328117
19.8844535888493,-74.7126104197568
26.6879142745222,-76.958176848711
33.4952557169926,-79.3278995922989
40.2944833599294,-81.9262651713276
47.0694601562491,-84.9094008880471
53.7952124646723,-88.5344734025131
60.4273718482956,-93.2696256023521
66.8747889267421,-100.06638393142
72.9164979387876,-111.099271120024
77.9171824366615,-131.562871850431";

        private static readonly CultureInfo EnUs = new CultureInfo("en-US");

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dataToUse = PolygonDataBad;
            //uncomment to draw similar circle which is not crashing
            //dataToUse = PolygonDataGood;
            var polygonPoints = dataToUse.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).
                Select(line =>
                {
                    var latLon = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(coordinate => double.Parse(coordinate, EnUs)).ToArray();
                    var point = new BasicGeoposition()
                    {
                        Latitude = latLon[0],
                        Longitude = latLon[1],
                    };
                    return point;
                }).ToArray();
            var geoboundingBox = GeoboundingBox.TryCompute(polygonPoints);
            var circle = new MapPolygon();
            circle.ZIndex = 1;
            circle.FillColor = Colors.IndianRed;
            circle.StrokeThickness = 0;
            circle.Path = new Geopath(polygonPoints);
            var allMapElements = myMap.MapElements.ToArray();
            foreach (var element in allMapElements)
            {
                myMap.MapElements.Remove(element);
            }
            myMap.MapElements.Add(circle);
            myMap.TrySetViewBoundsAsync(geoboundingBox, new Thickness(20), MapAnimationKind.Default);

            /*
            Circle-like polygon (50 or 51 points)
            Center 26.7717155814171,23.8236242160201
Radius (m) 9002647.96

72.3561218184286,-156.17637578398
71.0863065836736,-178.61932805177
67.7083192602375,163.484157692104
63.0063233971529,150.6199905179
57.5567116398686,141.369089841334
51.6843291542354,134.425969785395
45.5649053195061,128.939203029712
39.2975039698604,124.38568334369
32.9419453587829,120.437981094142
26.5373959112047,116.881531973797
20.1119509123273,113.567164936019
13.6879261361038,110.383660509321
7.28507974940788,107.241137819998
0.922863201177285,104.060215859841
-5.37769827020163,100.764150737761
-11.5921057945983,97.2723224766673
-17.690145981035,93.4940722445726
-23.6329756998278,89.3223100044802
-29.3691471539978,84.6268190551698
-34.8291578453229,79.2482111564971
-39.9182691085392,72.9958122880465
-44.5081121419994,65.6575513279139
-48.4298611142111,57.0375370800798
-51.4764473263076,47.0415777952807
-53.4267071210185,35.8100401292019
-54.1004470187372,23.8236242160201
-53.4267071210185,11.8372083028383
-51.4764473263076,0.605670636759506
-48.4298611142111,-9.39028864803959
-44.5081121419994,-18.0103028958736
-39.9182691085392,-25.3485638560062
-34.8291578453229,-31.6009627244569
-29.3691471539978,-36.9795706231297
-23.6329756998278,-41.67506157244
-17.690145981035,-45.8468238125325
-11.5921057945983,-49.625074044627
-5.37769827020163,-53.1169023057211
0.922863201177295,-56.4129674278006
7.2850797494079,-59.5938893879583
13.6879261361038,-62.7364120772804
20.1119509123273,-65.9199165039786
26.5373959112047,-69.2342835417569
32.9419453587829,-72.7907326621023
39.2975039698605,-76.7384349116494
45.5649053195062,-81.2919545976718
51.6843291542354,-86.7787213533546
57.5567116398686,-93.7218414092934
63.0063233971529,-102.97274208586
67.7083192602375,-115.836909260064
71.0863065836736,-133.73342351619
*/
    }
}
}
