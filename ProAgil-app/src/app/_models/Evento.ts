import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { User } from './User';

export class Evento {

    constructor() { }
    id: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    imagemURL: string;
    telefone: string;
    email: string;
    descricao: string;
    lotes: Lote[];
    redesSociais: RedeSocial[];
    userEventos: User[];
}
