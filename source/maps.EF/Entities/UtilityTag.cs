using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class UtilityTag
    {
        public int Id { get; set; }


        [StringLength(50)]
        public string Name { get; set; }


        public string Color
        {
            get
            {
                switch (Id)
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