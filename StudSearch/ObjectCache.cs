using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace StudSearch
{
    public class ObjectCache
    {
        /// <summary>
        /// Returns an unfiltered <see cref="List{T}"/> of <see cref="CourseArgs"/> from file
        /// </summary>
        /// <remarks>need to call safely</remarks>
        public static List<CourseArgs> CourseRootList { get { return JsonConvert.DeserializeObject<List<CourseArgs>>(File.ReadAllText(@"Resource\Courses.json")); } }
        /// <summary>
        /// Returns an unfiltered <see cref="List{T}"/> of <see cref="StudentArgs"/> from file
        /// </summary>
        /// <remarks>need to call safely</remarks>
        public static List<StudentArgs> StudentRootList { get { return JsonConvert.DeserializeObject<List<StudentArgs>>(File.ReadAllText(@"Resource\Students.json")); } }
    }
}
