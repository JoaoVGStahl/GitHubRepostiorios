export class RepositorioDTO {
    id: number;
    nome: string;
    url: string;
    descricao: string;
    dataCriacao: Date;
    dataUltimaAtualizacao: Date;
    stars: number;
    forks: number;
    watchers: number;
    issues: number;
    linguagem: string;
    favorito: boolean;


    constructor(
        id: number,
        nome: string,
        url: string,
        descricao: string,
        dataCriacao: Date,
        dataUltimaAtualizacao: Date,
        stars: number,
        forks: number,
        watchers: number,
        issues: number,
        linguagem: string,
        favorito: boolean,
    ) {
        this.id = id;
        this.nome = nome;
        this.url = url;
        this.descricao = descricao;
        this.dataCriacao = dataCriacao;
        this.dataUltimaAtualizacao = dataUltimaAtualizacao;
        this.stars = stars;
        this.forks = forks;
        this.watchers = watchers;
        this.issues = issues;
        this.linguagem = linguagem;
        this.favorito = favorito;
    }
}