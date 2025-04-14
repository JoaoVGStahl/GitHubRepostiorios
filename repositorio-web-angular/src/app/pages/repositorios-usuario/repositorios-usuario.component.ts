import { Component, OnInit } from '@angular/core';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepositorioItemComponent } from '../../shared/components/repositorio-item/repositorio-item.component';

@Component({
  selector: 'app-repositorios-usuario',
  imports: [CommonModule, FormsModule, RepositorioItemComponent],
  templateUrl: './repositorios-usuario.component.html',
  styleUrl: './repositorios-usuario.component.css'
})
export class RepositoriosUsuarioComponent implements OnInit {
  nomeUsuario: string = '';
  repositoriosRelevantes: RepositorioDTO[] = [];
  buscando = false;

  constructor(private repositorioService: RepositoriosApiProvider) { }

  ngOnInit(): void {
    this.nomeUsuario = 'JoaoVGStahl';
    this.buscar();
  }

  buscar(): void {
    if (this.nomeUsuario.trim() === '') {
      this.repositoriosRelevantes = [];
      return;
    }

    this.buscando = true;
    this.repositorioService.listarPorUsuario(this.nomeUsuario)
      .subscribe(repositorios => {
        this.repositoriosRelevantes = repositorios;
        this.buscando = false;
      });
  }
} 
