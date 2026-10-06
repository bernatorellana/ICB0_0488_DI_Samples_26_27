namespace Model
{
    public class Dept
    {
        public Dept(int dept_no, string dnom, string loc)
        {
            this.dept_no = dept_no;
            this.dnom = dnom;
            this.loc = loc;
        }

        public int dept_no { get; set; }
        public string dnom { get; set; }
        public string loc { get; set; }


    }
    
}
