﻿using System.Collections.Generic;

namespace Intersect.Migration.UpgradeInstructions.Upgrade_7.Intersect_Convert_Lib.GameObjects.Conditions
{
    public class ConditionLists
    {
        public List<ConditionList> Lists = new List<ConditionList>();

        public ConditionLists()
        {
        }

        public ConditionLists(Upgrade_10.Intersect_Convert_Lib.ByteBuffer buff)
        {
            Load(buff);
        }

        public ConditionLists(byte[] data)
        {
            Load(data);
        }

        public void Load(Upgrade_10.Intersect_Convert_Lib.ByteBuffer buff)
        {
            Lists.Clear();
            var count = buff.ReadInteger();
            for (int i = 0; i < count; i++)
            {
                var lst = new ConditionList();
                lst.Load(buff);
                Lists.Add(lst);
            }
        }

        public void Load(byte[] data)
        {
            var buff = new Upgrade_10.Intersect_Convert_Lib.ByteBuffer();
            buff.WriteBytes(data);
            Load(buff);
            buff.Dispose();
        }

        public void Save(Upgrade_10.Intersect_Convert_Lib.ByteBuffer buff)
        {
            buff.WriteBytes(Data());
        }

        public byte[] Data()
        {
            var buff = new Upgrade_10.Intersect_Convert_Lib.ByteBuffer();
            buff.WriteInteger(Lists.Count);
            for (int i = 0; i < Lists.Count; i++)
            {
                Lists[i].Save(buff);
            }
            return buff.ToArray();
        }
    }
}