import { Injectable } from "@angular/core";
import { Environment } from "../../../environment";
import { HttpClient } from "@angular/common/http";
import { RepositorioDTO } from "../../domain/models/repositorio.model";
import { Observable } from "rxjs/internal/Observable";

@Injectable({
    providedIn: 'root'
})
export class FavoritosApiProvider {
    private readonly API = Environment.apiUrl;
    private readonly baseUrl = `${this.API}Favoritos/`;

    constructor(private http: HttpClient) { }

    listar(): Observable<RepositorioDTO[]> {
        return this.http.get<RepositorioDTO[]>(`${this.baseUrl}ListarFavoritos`);
    }

    remover(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${this.baseUrl}RemoverFavorito?id=${id}`);
    }

    adicionar(repositorio: RepositorioDTO): Observable<boolean> {
        return this.http.post<boolean>(`${this.baseUrl}AdicionarFavorito`, repositorio);
    }
}