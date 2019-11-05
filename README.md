# Json2Net
Json2Net


# Json 格式文件 转 C# 文件

# 定义格式
# 第一层 NameSpace
# 第二层 ClassName
# 第三层 Member

#member 定义格式  Type => FiledName

#  一个json 文件只可以定义 一个namespace，一个namespace可以定义多个class

# 参考格式如下:

# Json:

{
	"NameSpace_t":
	{
		"ClassName_t":
		{
			"string":  "member_str",
			"int32": "member_int32"
		},
		"ClassName_t_1":
		{
			"string":  "member_str",
			"int32": "member_int32"
		},
		"ClassName_t_2":
		{
			"string":  "member_str",
			"Dictionary<int32, ClassName_t_1>": "member_int32",
			"List<int32>": "member_int32"
		}
	}
}

# C#：
namespace NameSpace_t
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    public partial class ClassName_t
    {
        public string member_str
        {
            get
            {
            }
            set
            {
            }
        }
        public int member_int32
        {
            get
            {
            }
            set
            {
            }
        }
    }
    public partial class ClassName_t_1
    {
        public string member_str
        {
            get
            {
            }
            set
            {
            }
        }
        public int member_int32
        {
            get
            {
            }
            set
            {
            }
        }
    }
    public partial class ClassName_t_2
    {
        public string member_str
        {
            get
            {
            }
            set
            {
            }
        }
        public Dictionary<int32, ClassName_t_1> member_int32
        {
            get
            {
            }
            set
            {
            }
        }
        public List<int32> member_int32
        {
            get
            {
            }
            set
            {
            }
        }
    }
}