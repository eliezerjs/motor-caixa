﻿using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM4Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK05()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK06()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public string GetImagePath(PrevidenciaM4Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
