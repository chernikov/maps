using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace maps.EF.Entities
{ 
    public class Social
    {
        public enum ProviderType
        {
            facebook = 0x01,
         /*   google = 0x02,
            vk = 0x03,
            twitter = 0x04*/
        }

        public int Id { get; set; }

        [Required]
        public string Identifier { get; set; }

        public int Provider { get; set; }

        public string UserInfo { get; set; }

        public string JsonResource { get; set; }

	}
}