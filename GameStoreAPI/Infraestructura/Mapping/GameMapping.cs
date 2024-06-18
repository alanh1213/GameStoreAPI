using GameStoreAPI.Aplicacion.Dtos;
using GameStoreAPI.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Infraestructura.Mapping
{
    public static class GameMapping
    {
        //*******************************************************************************************
        // Se extiende las clases Game y GameDto con metodos que mapean automaticamente los campos
        //*******************************************************************************************
        public static Game ToEntity(this CreateGameDto juego)
        {
            return new Game()
            {
                Nombre = juego.Nombre,
                GeneroId = juego.GeneroId,
                Precio = juego.Precio,
                FechaLanzamiento = juego.FechaLanzamiento
            };
        }

        public static Game ToEntity(this UpdateGameDto juego, int id)
        {
            return new Game()
            {
                Id = id,
                Nombre = juego.Nombre,
                GeneroId = juego.GeneroId,
                Precio = juego.Precio,
                FechaLanzamiento = juego.FechaLanzamiento
            };
        }

        //
        public static GameSummaryDto ToGameSummaryDto(this Game juego)
        {
            return new GameSummaryDto(
                    juego.Id,
                    juego.Nombre,
                    juego.Genero!.Nombre,
                    juego.Precio,
                    juego.FechaLanzamiento
            );
        }

        public static GameDetailsDto ToGameDetailsDto(this Game juego)
        {
            return new GameDetailsDto(
                    juego.Id,
                    juego.Nombre,
                    juego.GeneroId,
                    juego.Precio,
                    juego.FechaLanzamiento
            );
        }
    }
}
