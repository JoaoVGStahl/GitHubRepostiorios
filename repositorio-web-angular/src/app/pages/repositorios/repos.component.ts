import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepositoriosApiProvider } from '../../core/providers/repositorio.api.provider';
import { RepositorioDTO } from '../../domain/models/repositorio.model';
import { RepositorioItemComponent } from '../shareds/repositorio-item/repositorio-item.component';

@Component({
  selector: 'app-repositorios',
  standalone: true,
  imports: [CommonModule, FormsModule, RepositorioItemComponent],
  templateUrl: './repos.component.html',
  styleUrls: ['./repos.component.css']
})
export class RepositoriosComponent implements OnInit {
  nomeRepositorio: string = '';
  repositorios: RepositorioDTO[] = [];
  buscando = false;

  constructor(private repositorioService: RepositoriosApiProvider) { }

  ngOnInit(): void { }

  buscarRepos(): void {
    if (this.buscando) return;

    if (this.nomeRepositorio.trim() === '') {
      this.repositorios = [];
      return;
    }

    this.buscando = true;
    this.repositorioService.listar(this.nomeRepositorio)
      .subscribe(repositorios => {
        this.repositorios = repositorios;
        this.buscando = false;
      });
  }
} 