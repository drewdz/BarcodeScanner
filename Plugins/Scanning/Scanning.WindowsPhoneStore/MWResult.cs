using BarcodeLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MW
{
    class MWResult
    {
        public string text;
        public byte[] bytes;
        public int bytesLength;
        public int type;
        public int subtype;
        public int imageWidth;
        public int imageHeight;
        public bool isGS1;
        public MWLocation locationPoints;

        public MWResult()
        {
            text = null;
            bytes = null;
            bytesLength = 0;
            type = 0;
            subtype = 0;
            isGS1 = false;
            locationPoints = null;
            imageWidth = 0;
            imageHeight = 0;
        }

    }

    class MWResults
    {
        public int version;
        public List<MWResult> results;
        public int count;

        public MWResults(byte[] buffer)
        {
            results = new List<MWResult>();
            count = 0;
            version = 0;

            if (buffer[0] != 'M' || buffer[1] != 'W' || buffer[2] != 'R')
            {
                return;
            }

            version = buffer[3];

            count = buffer[4];

            int currentPos = 5;

            for (int i = 0; i < count; i++)
            {

                MWResult result = new MWResult();

                int fieldsCount = buffer[currentPos];
                currentPos++;
                for (int f = 0; f < fieldsCount; f++)
                {
                    int fieldType = buffer[currentPos];
                    int fieldNameLength = buffer[currentPos + 1];
                    int fieldContentLength = 256 * (buffer[currentPos + 3 + fieldNameLength] & 0xFF) + (buffer[currentPos + 2 + fieldNameLength] & 0xFF);
                    string fieldName = null;

                    if (fieldNameLength > 0)
                    {
                        fieldName = Encoding.UTF8.GetString(buffer, currentPos + 2, fieldNameLength);
                    }

                    int contentPos = currentPos + fieldNameLength + 4;
                    float[] locations = new float[8];
                    if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_TYPE)
                    {
                        result.type = BitConverter.ToInt32(buffer, contentPos);
                    }
                    else
                        if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_SUBTYPE)
                        {
                            result.subtype = BitConverter.ToInt32(buffer, contentPos);
                        }
                        else
                            if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_ISGS1)
                            {
                                result.isGS1 = BitConverter.ToInt32(buffer, contentPos) == 1;
                            }
                            else
                                if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_IMAGE_WIDTH)
                                {
                                    result.imageWidth = BitConverter.ToInt32(buffer, contentPos);
                                }
                                else
                                    if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_IMAGE_HEIGHT)
                                    {
                                        result.imageHeight = BitConverter.ToInt32(buffer, contentPos);
                                    }
                                    else
                                        if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_LOCATION)
                                        {
                                            for (int l = 0; l < 8; l++)
                                            {
                                                locations[l] = BitConverter.ToSingle(buffer, contentPos + l * 4);
                                            }
                                            result.locationPoints = new MWLocation(locations);
                                        }
                                        else
                                            if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_TEXT)
                                            {
                                                result.text = Encoding.UTF8.GetString(buffer, contentPos, fieldContentLength);
                                            }
                                            else
                                                if (fieldType == BarcodeLib.Scanner.MWB_RESULT_FT_BYTES)
                                                {
                                                    result.bytes = new byte[fieldContentLength];
                                                    result.bytesLength = fieldContentLength;
                                                    for (int c = 0; c < fieldContentLength; c++)
                                                    {
                                                        result.bytes[c] = buffer[contentPos + c];
                                                    }
                                                }
                    currentPos += (fieldNameLength + fieldContentLength + 4);
                }
                results.Add(result);
            }
        }

        public MWResult getResult(int index)
        {
            return results.ElementAt(index);
        }
    }
}
