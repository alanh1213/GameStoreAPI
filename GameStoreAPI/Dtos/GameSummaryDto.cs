﻿namespace GameStoreAPI.Dtos
{
    public record class GameSummaryDto(int Id, 
        string Nombre, 
        string Genero, 
        decimal Precio, 
        DateOnly FechaLanzamiento);
}
