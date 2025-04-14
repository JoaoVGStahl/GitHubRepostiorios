import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { Observable } from 'rxjs/internal/Observable';
import { Environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class RepositoriosApiProvider {
  private readonly API = Environment.apiUrl;
  private readonly baseUrl = `${this.API}Repositorios/`;

  constructor(private http: HttpClient) { }

  listar(nome: string): Observable<RepositorioDTO[]> {
    return this.http.get<RepositorioDTO[]>(`${this.baseUrl}ListarPorNome?nome=${nome}`);
  }

  listarRelevantes(nome: string, asc: boolean): Observable<RepositorioDTO[]> {
    return this.http.get<RepositorioDTO[]>(`${this.baseUrl}ListarPorRelevancia?nome=${nome}&asc=${asc}`);
  }

  listarPorUsuario(nome: string): Observable<RepositorioDTO[]> {
    return this.http.get<RepositorioDTO[]>(`${this.baseUrl}ListarDoUsuario?termo=${nome}`);
  }
}