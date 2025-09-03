using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Serializable2DArray<TDataType>
{
    [Serializable]
    public struct RowData
    {
        public TDataType[] row;

        public RowData(int size)
        {
            row = new TDataType[size];
        }
    }

    private int width = 0;
    public RowData[] rows;

    public Serializable2DArray(int width, int height)
    {
        this.width = width;

        rows = new RowData[height];

        for(int i = 0; i < height; i++)
        {
            rows[i] = new RowData(width);
        }
    }

    public void AddRowToArray()
    {
        int newRowsNumber = rows.Length + 1;

        RowData[] tempRows = new RowData[newRowsNumber];

        for(int i = 0; i < tempRows.Length; i++)
        {
            tempRows[i] = new RowData(width);
            
            for(int j = 0; j < width; j++)
            {
                tempRows[i].row[j] = rows[i].row[j];
            }
        }

        rows = new RowData[newRowsNumber];

        for(int i = 0; i < newRowsNumber; i++)
        {
            for(int j = 0; j < width; j++)
            {
                rows[i].row[j] = tempRows[i].row[j];
            }
        }
    }
}
