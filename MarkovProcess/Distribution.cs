// <copyright file="Distribution.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace MarkovProcess
{
        using System.Collections.Generic;

        /// <summary>
        /// Distribution of elements.
        /// </summary>
        /// <typeparam name="T">Type of elements to monitor and generate.</typeparam>
        public class Distribution<T>
        {
                /// <summary>
                /// The element counts.
                /// </summary>
                private readonly Dictionary<T, int> counts = new Dictionary<T, int>();

                /// <summary>
                /// The total count.
                /// </summary>
                private int totalCount;

                /// <summary>
                /// Initializes a new instance of the <see cref="MarkovProcess.Distribution{T}"/> class.
                /// </summary>
                public Distribution()
                {
                }

                /// <summary>
                /// Gets the total count.
                /// </summary>
                /// <value>The total count.</value>
                public int TotalCount
                {
                        get
                        {
                                return this.totalCount;
                        }
                }

                /// <summary>
                /// Add the specified element.
                /// </summary>
                /// <param name="what">What element.</param>
                /// <returns>The current count.</returns>
                public int Add(T what)
                {
                        if (this.counts.ContainsKey(what))
                        {
                                ++this.counts[what];
                        }
                        else
                        {
                                this.counts.Add(what, 1);
                        }

                        ++this.totalCount;
                        return this.counts[what];
                }

                /// <summary>
                /// Return the count for the specified element.
                /// </summary>
                /// <param name="what">The element to count.</param>
                /// <returns>The current count.</returns>
                public int Count(T what)
                {
                        return this.counts.ContainsKey(what) ? this.counts[what] : 0;
                }

                /// <summary>
                /// Get the element at the specified index.
                /// </summary>
                /// <returns>The index.</returns>
                /// <param name="max">The maximum index.</param>
                public T AtIndex(int max)
                {
                        int countDown = max;

                        foreach (T k in this.counts.Keys)
                        {
                                countDown -= this.counts[k];
                                if (countDown <= 0)
                                {
                                        return k;
                                }
                        }

                        return default(T);
                }
        }
}