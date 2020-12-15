using Gevlee.Swallow.Api.Contract.Tags;
using System;
using System.Collections.Generic;

namespace Gevlee.Swallow.Api.Contract.Tasks
{
    public class TaskModel
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public IEnumerable<TagModel> Tags { get; set; }

        public DateTime Date
        {
            get; set;
        }

        public bool IsActive
        {
            get; set;
        }

        public DateTime? ActiveSince
        {
            get; set;
        }

        public double ElapsedSeconds
        {
            get; set;
        }
    }
}
