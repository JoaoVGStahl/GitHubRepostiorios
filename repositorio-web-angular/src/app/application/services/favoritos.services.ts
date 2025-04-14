import { Observable } from "rxjs/internal/Observable";
import { RepositorioDTO } from "../../domain/models/repositorio.model";
import { Injectable } from "@angular/core";
import { FavoritosApiProvider } from "../../core/providers/favoritos.api.provider";

@Injectable({
    providedIn: 'root'
})
export class FavoritosService {
    constructor(private api: FavoritosApiProvider) { }

    listar(): Observable<RepositorioDTO[]> {
        return this.api.listar();
    }

    adicionar(repositorio: RepositorioDTO): Observable<boolean> {
        return this.api.adicionar(repositorio);
    }
    remover(id: number): Observable<boolean> {
        return this.api.remover(id);
    }
}