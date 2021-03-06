﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq.Expressions;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Resources;
    using System.Collections;

    /// <summary>
    /// Represents chart scatter (XY) series
    /// </summary>
    /// <typeparam name="TModel">The Chart model type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class ChartScatterSeries<TModel, TXValue, TYValue> : ChartSeriesBase<TModel>, IChartScatterSeries where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartScatterSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="expressionXValue">The X expression.</param>
        /// <param name="expressionYValue">The Y expression.</param>
        public ChartScatterSeries(Chart<TModel> chart, Expression<Func<TModel, TXValue>> xValueExpression, Expression<Func<TModel, TYValue>> yValueExpression)
            : base(chart)
        {
            Guard.IsNotNull(xValueExpression, "xValueExpression");
            Guard.IsNotNull(yValueExpression, "yValueExpression");

            if (typeof(TModel).IsPlainType() && !(xValueExpression.IsBindable() || yValueExpression.IsBindable()))
            {
                throw new InvalidOperationException(TextResource.MemberExpressionRequired);
            }

            XMember = xValueExpression.MemberWithoutInstance();
            YMember = yValueExpression.MemberWithoutInstance();

            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartScatterSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="data">The data.</param>
        public ChartScatterSeries(Chart<TModel> chart, IEnumerable data)
            : base(chart)
        {
            Guard.IsNotNull(data, "data");

            Data = data;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartScatterSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public ChartScatterSeries(Chart<TModel> chart)
            : base(chart)
        {
            Initialize();
        }

        /// <summary>
        /// Gets the model X data member name.
        /// </summary>
        /// <value>The model X data member name.</value>
        public string XMember
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the model Y data member name.
        /// </summary>
        /// <value>The model Y data member name.</value>
        public string YMember
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the X axis name to use for this series.
        /// </summary>
        public string XAxis
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y axis name to use for this series.
        /// </summary>
        public string YAxis
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the scatter chart data labels configuration
        /// </summary>
        public ChartPointLabels Labels
        {
            get;
            set;
        }

        /// <summary>
        /// The line chart markers configuration.
        /// </summary>
        public ChartMarkers Markers
        {
            get;
            set;
        }

        /// <summary>
        /// The scatter chart data source.
        /// </summary>
        public IEnumerable Data
        {
            get;
            set;
        }

        protected virtual void Initialize()
        {
            Labels = new ChartPointLabels();
            Markers = new ChartMarkers();
        }

        /// <summary>
        /// Creates a serializer for the series
        /// </summary>
        public override IChartSerializer CreateSerializer()
        {
            return new ChartScatterSeriesSerializer(this);
        }
    }
}