/**
 * PHR_SharePointResourceManagement
 *
 * Copyright (C) 1999-2014 Photon Infotech Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Phresco.FusionCharts
{
    /// <summary>
    /// Represents an array of plots to graph
    /// </summary>
    public class Plots
    {
        /// <summary>
        /// xValues storred in an array that contains string
        /// </summary>
        private ArrayList _xArray;
        /// <summary>
        /// yValues storred in an array that contains double
        /// </summary>
        private ArrayList _yArray;

        /// <summary>
        /// Constructor
        /// </summary>
        public Plots()
        {
            _xArray = new ArrayList();
            _yArray = new ArrayList();
        }

        /// <summary>
        /// Adds a new plot 
        /// </summary>
        /// <param name="x">a string for xValue</param>
        /// <param name="y">a double for yValue</param>
        public void Add(string x, double y)
        {
            _xArray.Add(x);
            _yArray.Add(y);
        }

        /// <summary>
        /// Gets an x value based on its position in the array
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public string GetX(int pos)
        {
            return _xArray[pos].ToString();
        }

        /// <summary>
        /// Gets an y value based on its position in the array
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public double GetY(int pos)
        {
            return (double)_yArray[pos];
        }

        /// <summary>
        /// Returns true of the x value exists. Returns false if not.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool Contains(string x)
        {
            return _xArray.Contains(x);
        }

        /// <summary>
        /// Gets the index (position) of a element x
        /// </summary>
        /// <param name="x"></param>
        /// <returns>-1 if the elements is not present in the array</returns>
        public int XIndexOf(string x)
        {
            if (_xArray.Contains(x)) return _xArray.IndexOf(x);
            return -1;
        }

        /// <summary>
        /// Sets the y value for an existing plot
        /// </summary>
        /// <param name="pos">The position of the plot to replace</param>
        /// <param name="value">The new y value</param>
        public void SetY(int pos, double value)
        {
            _yArray[pos] = value;
        }

        /// <summary>
        /// Returns the number of elements in the array
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _xArray.Count;
        }

    }
}
