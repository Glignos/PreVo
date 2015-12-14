using System;

namespace PreVo.Model
{
    public class Content : IPresentable
    {
        /* The content of each presentation page
        / it is divided into a) Static Content which
        / includes Images and Text and into b) Data
        / Content which applies only to datasets. */
        private int Id { get; set; }
        public String Description { get; set; }
    }
}