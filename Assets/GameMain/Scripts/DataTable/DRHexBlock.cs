//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-01-15 20:51:35.210
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UGFExtensions;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace OasisProject3D
{
    /// <summary>
    /// 基础地块表。
    /// </summary>
    public class DRHexBlock : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取地块名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取绿化度范围。
        /// </summary>
        public Vector2 GreennessRange
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取地块转化率。
        /// </summary>
        public float[] ConversionRate
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
			Name = columnStrings[index++];
			GreennessRange = DataTableExtension.ParseVector2(columnStrings[index++]);
			ConversionRate = DataTableExtension.ParseSingleArray(columnStrings[index++]);
            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    GreennessRange = binaryReader.ReadVector2();
					ConversionRate = binaryReader.ReadSingleArray();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
