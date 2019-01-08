
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JZ.Tools
{
    public static partial class ExtLinq
    {
        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return p => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return p => false;
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="TKey">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, propertyName), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateEqual<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateNotEqual<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThan<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName小于propertyValue 
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThan<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThanOrEqual<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=> p.propertyName 小于= propertyValue 
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThanOrEqual<T>(string propertyName, object propertyValue, Type typeValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeValue);//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetContains<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetNotContains<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }

        /// <summary>
        /// 功能描述:拼接Or
        /// 作　　者:beck.huang
        /// 创建日期:2018-11-30 15:35:10
        /// 任务编号:好餐谋后台管理系统
        /// </summary>
        /// <param name="expression1">expression1</param>
        /// <param name="expression2">expression2</param>
        /// <returns>返回值</returns>
        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return Compose(expression1, expression2, Expression.OrElse);
        }

        /// <summary>
        /// 功能描述:拼接And
        /// 作　　者:beck.huang
        /// 创建日期:2018-11-30 15:35:18
        /// 任务编号:好餐谋后台管理系统
        /// </summary>
        /// <param name="expression1">expression1</param>
        /// <param name="expression2">expression2</param>
        /// <returns>返回值</returns>
        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return Compose(expression1, expression2, Expression.AndAlso);
        }

        /// <summary>
        /// 功能描述:合并2个表达式
        /// 作　　者:beck.huang
        /// 创建日期:2018-11-30 15:35:26
        /// 任务编号:好餐谋后台管理系统
        /// </summary>
        /// <param name="first">first</param>
        /// <param name="second">second</param>
        /// <param name="merge">merge</param>
        /// <returns>返回值</returns>
        public static Expression<T> Compose<T>(Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        private class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;
            /// <summary>
            /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
            /// </summary>
            /// <param name="map">The map.</param>
            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            /// <summary>
            /// Replaces the parameters.
            /// </summary>
            /// <param name="map">The map.</param>
            /// <param name="exp">The exp.</param>
            /// <returns>Expression</returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }
    }

}
