import { Component } from '@angular/core';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { RepositorioItemComponent } from '../../shared/components/repositorio-item/repositorio-item.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-repositorios-relevantes',
  imports: [CommonModule, FormsModule, RepositorioItemComponent, MatIconModule],
  templateUrl: './repositorios-relevantes.component.html',
  styleUrl: './repositorios-relevantes.component.css'
})
export class RepositoriosRelevantesComponent {
  nomeRepositorio: string = '';
  repositorios: RepositorioDTO[] = [];
  buscando: boolean = false;
  asc: boolean = false;

  constructor(private repositorioService: RepositoriosApiProvider) { }

  buscar(): void {
    if (this.buscando) return;

    if (this.nomeRepositorio.trim() === '') {
      this.repositorios = [];
      return;
    }

    this.buscando = true;
    this.repositorioService.listarRelevantes(this.nomeRepositorio, this.asc)
      .subscribe(repositorios => {
        this.repositorios = repositorios;
        this.buscando = false;
      });
  }

  toogleOrdenacao() {
    this.asc = !this.asc;
    this.buscar();
  }
} 
