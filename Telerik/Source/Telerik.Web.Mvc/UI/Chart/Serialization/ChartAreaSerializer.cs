﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Extensions;

    internal class ChartAreaSerializer : IChartSerializer
    {
        private readonly ChartArea chartArea;

        public ChartAreaSerializer(ChartArea chartArea)
        {
            this.chartArea = chartArea;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();

            FluentDictionary.For(result)
                .Add("background", chartArea.Background, () => chartArea.Background.HasValue())
                .Add("margin", chartArea.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("border", chartArea.Border.CreateSerializer().Serialize(), ShouldSerializeBorder);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return chartArea.Margin.Top.HasValue ||
                   chartArea.Margin.Right.HasValue ||
                   chartArea.Margin.Bottom.HasValue ||
                   chartArea.Margin.Left.HasValue;
        }

        private bool ShouldSerializeBorder()
        {
            return chartArea.Border.Color.HasValue() ||
                   chartArea.Border.Width.HasValue ||
                   chartArea.Border.DashType.HasValue;
        }
    }
}