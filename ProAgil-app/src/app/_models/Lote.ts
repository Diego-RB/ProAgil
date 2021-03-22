export interface Lote {
    id: number;
    tipo: string;
    preco: number;
    dataInicio?: Date;
    dataFim?: Date;
    quantidade: number;
    eventoId: number;
}
