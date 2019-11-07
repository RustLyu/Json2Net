using System;
using System.Collections.Generic;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace Json2Net
{
	/// <summary>
	/// ref https://www.cnblogs.com/xszjk/articles/6414099.html
	/// </summary>
	class CodeGenerate
	{
		private static readonly string codeLanguage = "CSharp";
		public static void CSharpCode(string pathName, Dictionary<string, Dictionary<string, Dictionary<string, string>>> code)
		{
			// namespace
			foreach (var ns in code)
			{
				var compileUnit = new CodeCompileUnit();
				var nameSpace = new CodeNamespace(ns.Key);
				compileUnit.Namespaces.Add(nameSpace);
				nameSpace.Imports.Add(new CodeNamespaceImport("System"));
				nameSpace.Imports.Add(new CodeNamespaceImport("System.IO"));
				nameSpace.Imports.Add(new CodeNamespaceImport("System.Linq"));
				nameSpace.Imports.Add(new CodeNamespaceImport("System.Collections"));
				nameSpace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
				
				foreach (var cl in ns.Value)
				{
					// enum
					if (cl.Key.StartsWith("enum"))
					{
						var enumName = cl.Key.Split(' ');
						if (enumName[0].Equals("enum"))
						{
							var generateEnum = new CodeTypeDeclaration(enumName[1]); 
							nameSpace.Types.Add(generateEnum);
							generateEnum.IsEnum = true;
							foreach (var mem in cl.Value)
							{
								CodeMemberField m1 = new CodeMemberField();
								m1.Name = mem.Key;
								m1.Attributes = MemberAttributes.Public;
								generateEnum.Members.Add(m1);
							}
						}
					}
					// class
					else
					{
						var generateClass = new CodeTypeDeclaration(cl.Key);
						generateClass.IsClass = true;
						generateClass.IsPartial = true;

						nameSpace.Types.Add(generateClass);
						// member
						foreach (var mem in cl.Value)
						{
							var property = new CodeMemberProperty();
							property.Name = mem.Value;
							SetMemberType(property, mem.Key);
							property.HasSet = true;
							property.HasGet = true;
							property.Attributes = MemberAttributes.Final | MemberAttributes.Public;
							generateClass.Members.Add(property);
						}
					}
					CodeDomProvider provider = CodeDomProvider.CreateProvider(codeLanguage);
					CodeGeneratorOptions options = new CodeGeneratorOptions();
					options.BracingStyle = "C";
					options.BlankLinesBetweenMembers = false;
					using (var sw = new StreamWriter(pathName))
					{
						provider.GenerateCodeFromCompileUnit(compileUnit, sw, options);
					}
				}
			}
		}

		private static void SetMemberType(CodeMemberProperty property, string typeName)
		{
			//typeName = typeName.ToLower().Trim(' ');
			switch (typeName)
			{
				case "String":
				case "string":
					property.Type = new CodeTypeReference(typeof(string));
					break;
				case "int16":
				case "int16_t":
				case "int32":
				case "int32_t":
					property.Type = new CodeTypeReference(typeof(int));
					break;
				case "int64":
					property.Type = new CodeTypeReference(typeof(long));
					break;
				case "uint32":
					property.Type = new CodeTypeReference(typeof(uint));
					break;
				case "uint64":
					property.Type = new CodeTypeReference(typeof(ulong));
					break;
				case "float":
					property.Type = new CodeTypeReference(typeof(float));
					break;
				default:
					property.Type = new CodeTypeReference(typeName);
					break;
					//throw new Exception(string.Format("Not Impletion of Type：{0}", typeName));
			}
		}
	}
}
