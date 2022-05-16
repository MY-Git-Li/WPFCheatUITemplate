using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.Core.GameFuns
{
   public class GameDataAddress
   {
        IntPtr startAddress;
        //开始地址，偏移地址
        List<IntPtr> AddressOffset;
      
        IntPtr handle;
        bool isIntptr;

        IntPtr lastOffset;

        IntPtr endAddress;
        public GameDataAddress(IntPtr handle, IntPtr baseAddress)
        {
            AddressOffset = new List<IntPtr>();

            this.startAddress = baseAddress;
            this.handle = handle;

            endAddress = startAddress;

            isIntptr = false;
        }
        public GameDataAddress(IntPtr handle, IntPtr baseAddress, uint[] offset)
        {
            AddressOffset = new List<IntPtr>();

            this.startAddress = baseAddress;
            this.handle = handle;
            isIntptr = true;

           

            AddressOffset.Add(baseAddress);
            for (int i = 0; i < offset.Length; i++)
            {
                AddressOffset.Add((IntPtr)offset[i]);
            }

            lastOffset = AddressOffset[AddressOffset.Count - 1];
            AddressOffset.RemoveAt(AddressOffset.Count - 1);

            GetAddress();
        }

        void GetAddress()
        {

            IntPtr[] add = AddressOffset.ToArray();

            endAddress = (IntPtr)(lastOffset.ToInt64() + CheatTools.ReadMemory<IntPtr>(handle, add).ToInt64());
        }

        public IntPtr Address
        {
            get
            {
                if (isIntptr)
                {
                    GetAddress();
                }
                return endAddress;
            }

        }
    }
}
