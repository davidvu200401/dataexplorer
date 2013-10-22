﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataExplorer.Presentation.Core.Canvas.Items;

namespace DataExplorer.Presentation.Core.Geometry
{
    public interface IGeometryFactory
    {
        CanvasCircle CreateCircle(Rect shapeExtent);
    }
}
