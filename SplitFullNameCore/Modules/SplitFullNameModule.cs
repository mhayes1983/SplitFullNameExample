using System;
using System.Text.RegularExpressions;
using SplitFullNameCore.Interfaces;
using SplitFullNameCore.ViewModels;

namespace SplitFullNameCore.Modules
{
    public class SplitFullNameModule : ISplitFullName
    {
		public SplitFullNameModule()
		{ }

		public IName SplitFullName(string fullName)
		{
			string strFirstName = null;
			string strMiddleName = null;
			string strLastName = null;
			try
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    string strName = fullName.Trim();
                    //Remove double spaces
                    strName = Regex.Replace(strName, "\\s{2,}", " ");

                    int intPos = strName.IndexOf(",");
                    string[] strTokens;
					if (intPos > 0)
					{
						//Last, First Middle format
						strLastName = strName.Substring(0, intPos);
						strName = strName.Substring(intPos + 1).Trim();
						strTokens = strName.Split(' ');

						if (strTokens.Length == 1)
						{
							strFirstName = strTokens[0];
							strMiddleName = string.Empty;
						}
						else if (strTokens.Length == 2)
						{
							switch (strTokens[1].ToUpper())
							{
								case "JR":
								case "SR":
								case "JR.":
								case "SR.":
								case "II":
								case "LL":
									strFirstName = strName;
									strMiddleName = string.Empty;
									break;
								default:
									strFirstName = strTokens[0];
									strMiddleName = strTokens[1].Replace(".", string.Empty);
									break;
							}
						}
						else
						{
							strFirstName = strName;
							strMiddleName = string.Empty;
						}
					}
					else
					{
						// First Middle Last format
						strTokens = strName.Split(' ');

						switch (strTokens.Length)
						{
							case 2:
								//First and Last Name only
								strFirstName = strTokens[0];
								strLastName = strTokens[1];
								strMiddleName = string.Empty;
								break;
							case 3:
								//First Middle Last
								strFirstName = strTokens[0];
								strMiddleName = strTokens[1].Replace(".", string.Empty);
								strLastName = strTokens[2];
								break;
							case 4:
								//First Middle Mulitple Part Last Name
								strFirstName = strTokens[0];
								if (strTokens[1].Length > 0)
								{
									strMiddleName = strTokens[1].Replace(".", string.Empty);
								}
								else if (strTokens[2].Length > 0)
								{
									strMiddleName = strTokens[2].Replace(".", string.Empty);
								}
								if (!string.IsNullOrEmpty(strTokens[2]))
								{
									strLastName = string.Format("{0} {1}", strTokens[2], strTokens[3]);
								}
								else
								{
									strLastName = strTokens[3];
								}
								break;
							default:
								//Longer names
								//Determine if there is a middle initial
								//TODO: logic for this code can probably be updated to work with all First Middle Last formats
								bool blnContainsMiddleInitial = false;
								int intPositionWithMiddleInitial = 0;
								for (int i = 0; i <= strTokens.Length - 1; i++)
								{
									//We will assume the middle initial either is just 1 char or has a period and 2 chars
									if ((strTokens[i].Contains(".") && strTokens[i].Length == 2) || strTokens[i].Length == 1)
									{
										intPositionWithMiddleInitial = i;
										blnContainsMiddleInitial = true;
										break;
									}
								}

								//Start the first name with the first position
								strFirstName = strTokens[0];
								strMiddleName = string.Empty;

								int intStartPositionForLastName = 1;
								if (blnContainsMiddleInitial)
								{
									//construct the first name positions with the remaining positions up to Middle Initial
									for (int i = 1; i <= intPositionWithMiddleInitial - 1; i++)
									{
										strFirstName += (" " + strTokens[i]);
									}

									//get the middle initial and remove any period
									strMiddleName = strTokens[intPositionWithMiddleInitial].Replace(".", string.Empty);

									//determine the start position for the last name
									intStartPositionForLastName = intPositionWithMiddleInitial + 1;
								}

								//construct the last name with the remaining positions
								for (int i = intStartPositionForLastName; i <= strTokens.Length - 1; i++)
								{
									if (string.IsNullOrEmpty(strLastName))
									{
										strLastName += strTokens[i];
									}
									else
									{
										strLastName += (" " + strTokens[i]);
									}
								}

								break;
						}
					}
				}
            }
            catch (Exception)
            {
				//Logging would be nice
				strFirstName = null;
				strMiddleName = null;
				strLastName = null;
			}

			IName vmName = new NameViewModule();
			vmName.FirstName = strFirstName;
			vmName.MiddleName = strMiddleName;
			vmName.LastName = strLastName;

			return vmName;
        }
	}
}
