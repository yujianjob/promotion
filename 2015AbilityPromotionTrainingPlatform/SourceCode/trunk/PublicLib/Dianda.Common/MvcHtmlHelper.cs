using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Dianda.Common
{
    public static class HtmlExtensions
    {
        public static IHtmlString DescriptionFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression
        )
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var description = metadata.Description;
            return new HtmlString(description);
        }
    }
}
