﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class TextBoxBaseHtmlBuilder<T> : ITextBoxBaseHtmlBuilder where T : struct 
    {
        public TextBoxBaseHtmlBuilder(TextBoxBase<T> input)
        {
            Input = input;
        }

        public TextBoxBase<T> Input
        { 
            get; 
            private set; 
        }

        public IHtmlNode Build(string objectName)
        {
            return new HtmlElement("div")
                   .Attributes(Input.HtmlAttributes)
                   .PrependClass(UIPrimitives.Widget, objectName)
                   .ToggleClass("t-state-disabled", !Input.Enabled);
        }

        public IHtmlNode InputTag()
        {
            Func<object, T?> converter = val => 
            {
                return ((T)Convert.ChangeType(val, typeof(T))).AsNullable();
            };

            string value = Input.GetAttemptedValue();
            if (value == null)
            {
                T? result = Input.GetValue(converter);
                if (result != null)
                {
                    value = "{0}".FormatWith(result);
                }
            }
            
            return new HtmlElement("input", TagRenderMode.SelfClosing)
                   .Attributes(new { name = Input.Name, id = Input.Id, type = "text" })
                   .ToggleAttribute("value", value, value.HasValue())
                   .ToggleAttribute("disabled", "disabled", !Input.Enabled)
                   .Attributes(Input.InputHtmlAttributes)
                   .Attributes(Input.GetUnobtrusiveValidationAttributes())
                   .ToggleClass("input-validation-error", !Input.IsValid())
                   .PrependClass(UIPrimitives.Input);
        }

        public IHtmlNode UpButtonTag()
        {
            string title = string.IsNullOrEmpty(Input.ButtonTitleUp) ? "Increase value" : Input.ButtonTitleUp;

            return new HtmlElement("a")
                   .Attributes(new { href = "#", title = title, tabindex = "-1" })
                   .AddClass(UIPrimitives.Link, UIPrimitives.Icon, "t-arrow-up")
                   .Text("Increment");
        }

        public IHtmlNode DownButtonTag()
        {
            string title = string.IsNullOrEmpty(Input.ButtonTitleDown) ? "Decrease value" : Input.ButtonTitleDown;

            return new HtmlElement("a")
                   .Attributes(new { href = "#", title = title, tabindex = "-1" })
                   .AddClass(UIPrimitives.Link, UIPrimitives.Icon, "t-arrow-down")
                   .Text("Decrement");
        }
    }
}
