# SVGChart

Small sample about how to create a chart using a SVG file and SkiaSharp to display it.  
  
Just create a list of segments with the percentage and color 

```C#
new List<Tuple<int, string>>()
{
    new Tuple<int, string>(86, "#ff8787"),
    new Tuple<int, string>(100, "#42c0ff"),
    new Tuple<int, string>(37, "#b5f547"),
    new Tuple<int, string>(100, "#ffa14f"),
    new Tuple<int, string>(10, "#40bf40"),
    new Tuple<int, string>(55, "#ff4d4d")
};
```

For Line Chart just add a list of points

```C#
new List<int>()
{
   66,34,88,20,90,55,65,80,45
};
```

## Chart Options

- Horizontal Bar Chart.
- Donut Chart.
- Vertical Bar Chart.
- Line Chart

<p float="left">
  <img src="Readme/horizontalChart.png" width="250">
  <img src="Readme/donutChart.png" width="250">
  <img src="Readme/verticalChart.png" width="250">
  <img src="Readme/lineChart.png" width="250">
</p>


