﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System.ComponentModel;
    using System.Text;

    internal interface IParent
    {
        int[] NumberPath { get; }

        void SetHasChanges();

        string GetPath();

        void AppendPath(StringBuilder builder);

        void OnPropertyChanged(PropertyChangedEventArgs e);
    }
}