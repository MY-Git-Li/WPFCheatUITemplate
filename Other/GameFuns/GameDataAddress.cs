using System;
using System.Collections.Generic;

namespace CheatUITemplt
{
   public class GameDataAddress
   {
        int startAddress;
        //开始地址，偏移地址

        List<int> addressList;
        List<int> offsetAddress;

        int endAddress;
        IntPtr handle;
        bool isIntptr;

        public GameDataAddress(IntPtr handle, uint baseAddress)
        {
            this.startAddress = (int)baseAddress;
            this.handle = handle;
            endAddress = startAddress;
            isIntptr = false;
        }
        public GameDataAddress(IntPtr handle, uint baseAddress, uint[] offset)
        {

            this.startAddress = (int)baseAddress;
            this.handle = handle;
            isIntptr = true;
            offsetAddress = new List<int>();
            addressList = new List<int>();


            for (int i = 0; i < offset.Length; i++)
            {
                offsetAddress.Add((int)offset[i]);
            }

            GetAddress();
        }

        void GetAddress()
        {
            addressList.Clear();
            addressList.Add(CheatTools.ReadMemoryValue(startAddress, handle));

            for (int i = 1; i < offsetAddress.Count; i++)
            {
                addressList.Add(CheatTools.ReadMemoryValue(addressList[i - 1] + offsetAddress[i - 1], handle));
            }
            endAddress = addressList[addressList.Count - 1] + offsetAddress[offsetAddress.Count - 1];
        }

        public int Address
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
