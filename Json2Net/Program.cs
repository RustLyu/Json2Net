using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Serialization.CS;

namespace Json2Net
{
	public class Program
	{
		/// <summary>
		/// 3层 Dictionary namespace => class => member
		/// </summary>
		// private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> classValue = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
		static void Main(string[] args)
		{
			string outPath = null; // -o{FILE}, --descriptor_set_out={FILE}
			bool version = false; // --version
			bool help = false; // -h, --help
			var importPaths = new List<string>(); // -I{PATH}, --proto_path={PATH}
			var inputFiles = new List<string>(); // {PROTO_FILES} (everything not `-`)
			bool exec = false;
			string package = null; // --package=foo
			List<string> filesPath = new List<string>();
			Dictionary<string, string> options = null;
			foreach (string arg in args)
			{
				string lhs = arg, rhs = "";
				int index = arg.IndexOf('=');
				if (index > 0)
				{
					lhs = arg.Substring(0, index);
					rhs = arg.Substring(index + 1);
				}
				else if (arg.StartsWith("-o"))
				{
					lhs = "--descriptor_set_out";
					rhs = arg.Substring(2);
				}
				else if (arg.StartsWith("-I"))
				{
					lhs = "--json_path";
					rhs = arg.Substring(2);
				}

				if (lhs.StartsWith("+"))
				{
					if (options == null) options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					options[lhs.Substring(1)] = rhs;
					continue;
				}

				switch (lhs)
				{
					case "":
						break;
					case "--version":
						Console.WriteLine("1.0.0");
						break;
					case "--package":
						package = rhs;
						break;
					case "-h":
					case "--help":
						help = true;
						break;
					case "--csharp_out":
						outPath = rhs;
						exec = true;
						break;
					case "--descriptor_set_out":
						outPath = rhs;
						exec = true;
						break;
					case "--json_path":
						importPaths.Add(rhs);
						break;
					default:
						if (lhs.StartsWith("-") || !string.IsNullOrWhiteSpace(rhs))
						{
							help = true;
							break;
						}
						else
						{
							inputFiles.Add(lhs);
						}
						break;
				}
			}

			if (help)
			{
				return;
			}
			else if (version)
			{
				Console.WriteLine("1.0.0");
				return;
			}
			else if (inputFiles.Count == 0)
			{
				Console.Error.WriteLine("Missing input file.");
				return;
			}
			else if (!exec)
			{
				Console.Error.WriteLine("Missing output directives.");
				return;
			}
			else
			{
				if (importPaths.Count == 0)
				{
					Console.WriteLine("import path empty");
				}
				else
				{
					foreach (var dir in importPaths)
					{
						if (!Directory.Exists(dir))
						{
							Console.Error.WriteLine($"Directory not found: {dir}");
							return;
						}
					}
				}

				if (inputFiles.Count == 1 && importPaths.Count == 1)
				{
					SearchOption? searchOption = SearchOption.AllDirectories;

					if (searchOption != null)
					{
						//inputFiles.Clear();
						var searchRoot = importPaths[0];
						foreach (var path in Directory.EnumerateFiles(importPaths[0], inputFiles[0], searchOption.Value))
						{
							filesPath.Add(path);
						}
					}
				}
			}
			foreach (var path in filesPath)
			{
				string text = File.ReadAllText(path);
				var fileName = "\\" + path.Split('\\').Last().Split('.').First() + ".cs";
				try
				{
					DynamicJsonObject dy = ConvertJson(text);
					CodeGenerate.CSharpCode(outPath + fileName, dy);
					Console.WriteLine("JsonConvert2Net " + path + " => " + outPath + fileName + " Success");
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}
			}
			Console.ReadKey();
		}

		static DynamicJsonObject ConvertJson(string json)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
			DynamicJsonObject dy = jss.Deserialize(json, typeof(object)) as dynamic;
			return dy;
		}
	}
}
