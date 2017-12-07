using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品领用信息
    /// 创建日期：2013-09-13
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandCollarAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = "select Count(*) from T_OA_PARTSTAND_COLLAR " + this.BuildWhereStatement(tOaPartstandCollar);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartstandCollarVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_OA_PARTSTAND_COLLAR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TOaPartstandCollarVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartstandCollar">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartstandCollarVo Details(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = String.Format("select * from  T_OA_PARTSTAND_COLLAR " + this.BuildWhereStatement(tOaPartstandCollar));
            return SqlHelper.ExecuteObject(new TOaPartstandCollarVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartstandCollarVo> SelectByObject(TOaPartstandCollarVo tOaPartstandCollar, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_OA_PARTSTAND_COLLAR " + this.BuildWhereStatement(tOaPartstandCollar));
            return SqlHelper.ExecuteObjectList(tOaPartstandCollar, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartstandCollarVo tOaPartstandCollar, int iIndex, int iCount)
        {

            string strSQL = " select * from T_OA_PARTSTAND_COLLAR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tOaPartstandCollar));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartstandCollar"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = "select * from T_OA_PARTSTAND_COLLAR " + this.BuildWhereStatement(tOaPartstandCollar);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns></returns>
        public TOaPartstandCollarVo SelectByObject(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = "select * from T_OA_PARTSTAND_COLLAR " + this.BuildWhereStatement(tOaPartstandCollar);
            return SqlHelper.ExecuteObject(new TOaPartstandCollarVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartstandCollar, TOaPartstandCollarVo.T_OA_PARTSTAND_COLLAR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象领用明细和扣除库存数量
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandCollarVo tOaPartstandCollar, bool b)
        {
            ArrayList list = new ArrayList();
            string strSQL = SqlHelper.BuildInsertExpress(tOaPartstandCollar, TOaPartstandCollarVo.T_OA_PARTSTAND_COLLAR_TABLE);
            list.Add(strSQL);
            strSQL = "UPDATE T_OA_PARTSTAND_INFO SET INVENTORY=Convert(int,INVENTORY)-" + tOaPartstandCollar.USED_QUANTITY + " WHERE ID='" + tOaPartstandCollar.SAMPLE_ID + "'";
            list.Add(strSQL);

            return SqlHelper.ExecuteSQLByTransaction(list);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandCollar">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartstandCollar, TOaPartstandCollarVo.T_OA_PARTSTAND_COLLAR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tOaPartstandCollar.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandCollar_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartstandCollar_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandCollarVo tOaPartstandCollar_UpdateSet, TOaPartstandCollarVo tOaPartstandCollar_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tOaPartstandCollar_UpdateSet, TOaPartstandCollarVo.T_OA_PARTSTAND_COLLAR_TABLE);
            strSQL += this.BuildWhereStatement(tOaPartstandCollar_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_OA_PARTSTAND_COLLAR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TOaPartstandCollarVo tOaPartstandCollar)
        {
            string strSQL = "delete from T_OA_PARTSTAND_COLLAR ";
            strSQL += this.BuildWhereStatement(tOaPartstandCollar);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tOaPartstandCollar"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TOaPartstandCollarVo tOaPartstandCollar)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tOaPartstandCollar)
            {

                //编号
                if (!String.IsNullOrEmpty(tOaPartstandCollar.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tOaPartstandCollar.ID.ToString()));
                }
                //物料ID
                if (!String.IsNullOrEmpty(tOaPartstandCollar.SAMPLE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_ID = '{0}'", tOaPartstandCollar.SAMPLE_ID.ToString()));
                }
                //领用数量
                if (!String.IsNullOrEmpty(tOaPartstandCollar.USED_QUANTITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USED_QUANTITY = '{0}'", tOaPartstandCollar.USED_QUANTITY.ToString()));
                }
                //领用人ID
                if (!String.IsNullOrEmpty(tOaPartstandCollar.USER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USER_ID = '{0}'", tOaPartstandCollar.USER_ID.ToString()));
                }
                //领用日期
                if (!String.IsNullOrEmpty(tOaPartstandCollar.LASTIN_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LASTIN_DATE = '{0}'", tOaPartstandCollar.LASTIN_DATE.ToString()));
                }
                //领用理由
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REASON.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REASON = '{0}'", tOaPartstandCollar.REASON.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tOaPartstandCollar.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tOaPartstandCollar.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tOaPartstandCollar.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tOaPartstandCollar.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tOaPartstandCollar.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        /// <summary>
        /// 获取领用样品明细
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionPartByTable(TOaPartstandCollarVo tOaPartstandCollar, TOaPartstandInfoVo tOaPartstandInfor, int iIndex, int iCount)
        {
            string strSQL = @" SELECT A.ID,A.SAMPLE_ID,A.USED_QUANTITY,A.USER_ID,A.LASTIN_DATE,A.REASON,B.SAMPLE_NAME,B.SAMPLE_CODE,B.UNIT,C.REAL_NAME,C.USER_NAME
                                           FROM T_OA_PARTSTAND_COLLAR A
                                           LEFT JOIN T_OA_PARTSTAND_INFO B ON B.ID=A.SAMPLE_ID 
                                           LEFT JOIN T_SYS_USER C ON C.ID=A.USER_ID WHERE 1=1";

            if (!String.IsNullOrEmpty(tOaPartstandInfor.ID))
            {
                strSQL += String.Format(" AND B.ID = '{0}'", tOaPartstandInfor.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartstandInfor.SAMPLE_CODE))
            {
                strSQL += String.Format(" AND B.SAMPLE_CODE LIKE '%{0}%'", tOaPartstandInfor.SAMPLE_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartstandInfor.SAMPLE_NAME))
            {
                strSQL += String.Format(" AND B.SAMPLE_NAME LIKE '%{0}%'", tOaPartstandInfor.SAMPLE_NAME);
            }
            if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK4) && !String.IsNullOrEmpty(tOaPartstandCollar.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.LASTIN_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartstandCollar.REMARK4, tOaPartstandCollar.REMARK5);

            }
            if (!String.IsNullOrEmpty(tOaPartstandCollar.USER_ID))
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '%{0}%'", tOaPartstandCollar.USER_ID);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取领用样品明细总记录数
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetUnionPartByTableCount(TOaPartstandCollarVo tOaPartstandCollar, TOaPartstandInfoVo tOaPartstandInfor)
        {
            string strSQL = @" SELECT A.ID,A.SAMPLE_ID,A.USED_QUANTITY,A.USER_ID,A.LASTIN_DATE,A.REASON,B.SAMPLE_NAME,B.SAMPLE_CODE,C.REAL_NAME,C.USER_NAME
                                           FROM T_OA_PARTSTAND_COLLAR A
                                           LEFT JOIN T_OA_PARTSTAND_INFO B ON B.ID=A.SAMPLE_ID 
                                           LEFT JOIN T_SYS_USER C ON C.ID=A.USER_ID WHERE 1=1";

            if (!String.IsNullOrEmpty(tOaPartstandInfor.ID))
            {
                strSQL += String.Format(" AND B.ID = '{0}'", tOaPartstandInfor.ID);
            }
            if (!String.IsNullOrEmpty(tOaPartstandInfor.SAMPLE_CODE))
            {
                strSQL += String.Format(" AND B.SAMPLE_CODE LIKE '%{0}%'", tOaPartstandInfor.SAMPLE_CODE);
            }
            if (!String.IsNullOrEmpty(tOaPartstandInfor.SAMPLE_NAME))
            {
                strSQL += String.Format(" AND B.SAMPLE_NAME LIKE '%{0}%'", tOaPartstandInfor.SAMPLE_NAME);
            }
            if (!String.IsNullOrEmpty(tOaPartstandCollar.REMARK4) && !String.IsNullOrEmpty(tOaPartstandCollar.REMARK5))
            {
                strSQL += " AND CONVERT(DATETIME, CONVERT(VARCHAR(100), A.LASTIN_DATE,23),111)  ";
                strSQL += String.Format("  BETWEEN  CONVERT(DATETIME, CONVERT(varchar(100), '{0}',23),111) AND CONVERT(DATETIME, CONVERT(varchar(100), '{1}',23),111)", tOaPartstandCollar.REMARK4, tOaPartstandCollar.REMARK5);

            }
            if (!String.IsNullOrEmpty(tOaPartstandCollar.USER_ID))
            {
                strSQL += String.Format(" AND C.REAL_NAME LIKE '%{0}%'", tOaPartstandCollar.USER_ID);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        #endregion
    }

}
