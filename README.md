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
	"PackageName":
	{
		"enum EnumTestName":
		{
			"EnumTestName_None" : 0,
			"EnumTestName_Test" : 1
		},
		"ClassName_t":
		{
			"member_str" : "string",
			"member_int_1" : "int32",
			"member_int_2" : "int32",
			"member_int_3" : "int32",
			"member_int_4" : "int32"
		}
	}
}

# C#：
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PackageName
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    public enum EnumTestName
    {
        EnumTestName_None,
        EnumTestName_Test,
    }
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
        public int member_int_1
        {
            get
            {
            }
            set
            {
            }
        }
        public int member_int_2
        {
            get
            {
            }
            set
            {
            }
        }
        public int member_int_3
        {
            get
            {
            }
            set
            {
            }
        }
        public int member_int_4
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
