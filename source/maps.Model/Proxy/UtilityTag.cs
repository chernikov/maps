using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maps.Model
{ 
    public partial class UtilityTag
    {
        public string Color
        {
            get
            {
                switch (ID)
                {
                    case 1: return ""; //�������� ���� - ����
                    case 2: return "alert-info"; //��������� - �������
                    case 3: return "alert-danger"; //������� �������������� - ��������
                    case 4: return "alert-success"; //���������� ���� - �������
                    case 5: return "alert-warning"; //��������� - ������
                    case 6: return "alert-success"; //����� - ����

                    default: return "";
                }
            }
        }
	}
}