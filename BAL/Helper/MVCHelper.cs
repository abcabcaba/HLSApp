using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BAL.Helper
{
   public static class MVCHelper
    {


        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.DisplayName).Replace("\n", "<br />\n");

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }

       
        public static IHtmlString UnencodedLabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var text = (metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>()));
            if (string.IsNullOrEmpty(text))
            {
                return MvcHtmlString.Empty;
            }
            var tagBuilder = new TagBuilder("label");
            tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tagBuilder.InnerHtml = text;
            return new HtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}
