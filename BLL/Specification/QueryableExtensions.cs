using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Specification
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> SelectMembers<T>(this IQueryable<T> source, params string[] memberNames)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var bindings = memberNames
                .Select(name => Expression.PropertyOrField(parameter, name))
                .Select(member => Expression.Bind(member.Member, member));
            var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var selector = Expression.Lambda<Func<T, T>>(body, parameter);
            return source.Select(selector);
        }

        public static List<string> GetReturningFields<T>(List<Expression<Func<T, object>>> fields)
        {

            List<string> expressionFields = new List<string>();

            fields.ForEach(x => expressionFields.Add(x.Body.ToString()
                .Replace("Convert(", "")
                .Replace(", Object)", "")));

            List<string> returningFields = new List<string>();
            expressionFields.ForEach(s =>
            {
                returningFields.Add(s.Substring(s.IndexOf(".") + 1, (s.Length - s.IndexOf(".")) - 1));

            });

            return returningFields;

        }
    }
}
