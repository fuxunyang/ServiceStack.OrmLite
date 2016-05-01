﻿using System;
using System.Data;

namespace ServiceStack.OrmLite.Legacy
{
    public static class OrmLiteWriteExpressionsApiLegacy
    {
        /// <summary>
        /// Insert only fields in POCO specified by the SqlExpression lambda. E.g:
        /// <para>db.InsertOnly(new Person { FirstName = "Amy", Age = 27 }, q =&gt; q.Insert(p =&gt; new { p.FirstName, p.Age }))</para>
        /// </summary>
        [Obsolete("Use db.InsertOnly(obj, db.From<T>())")]
        public static void InsertOnly<T>(this IDbConnection dbConn, T obj, Func<SqlExpression<T>, SqlExpression<T>> onlyFields)
        {
            dbConn.Exec(dbCmd => dbCmd.InsertOnly(obj, onlyFields));
        }

        /// <summary>
        /// Use an SqlExpression to select which fields to update and construct the where expression, E.g: 
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ" }, ev => ev.Update(p => p.FirstName).Where(x => x.FirstName == "Jimi"));
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("FirstName" = 'Jimi')
        /// 
        ///   What's not in the update expression doesn't get updated. No where expression updates all rows. E.g:
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ", LastName = "Hendo" }, ev => ev.Update(p => p.FirstName));
        ///   UPDATE "Person" SET "FirstName" = 'JJ'
        /// </summary>
        [Obsolete("Use db.UpdateOnly(model, db.From<T>())")]
        public static int UpdateOnly<T>(this IDbConnection dbConn, T model, Func<SqlExpression<T>, SqlExpression<T>> onlyFields)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateOnly(model, onlyFields));
        }

        /// <summary>
        /// Flexible Update method to succinctly execute a free-text update statement using optional params. E.g:
        /// 
        ///   db.Update&lt;Person&gt;(set:"FirstName = {0}".Params("JJ"), where:"LastName = {0}".Params("Hendrix"));
        ///   UPDATE "Person" SET FirstName = 'JJ' WHERE LastName = 'Hendrix'
        /// </summary>
        [Obsolete(Messages.LegacyApi)]
        public static int UpdateFmt<T>(this IDbConnection dbConn, string set = null, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateFmt<T>(set, where));
        }

        /// <summary>
        /// Flexible Update method to succinctly execute a free-text update statement using optional params. E.g.
        /// 
        ///   db.Update(table:"Person", set: "FirstName = {0}".Params("JJ"), where: "LastName = {0}".Params("Hendrix"));
        ///   UPDATE "Person" SET FirstName = 'JJ' WHERE LastName = 'Hendrix'
        /// </summary>
        [Obsolete(Messages.LegacyApi)]
        public static int UpdateFmt(this IDbConnection dbConn, string table = null, string set = null, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateFmt(table, set, where));
        }
    }
}