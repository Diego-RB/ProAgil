import { Evento } from "./Evento";


export class User {

    id: number;
    userName: string;
    email: string;
    password: string;
    fullName: string;
    phoneNumber: number;
    profissao: string;
    Eventos: Evento[];
    imagemURL: string;
}
