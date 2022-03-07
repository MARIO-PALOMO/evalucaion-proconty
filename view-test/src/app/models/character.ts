export class character {
    id: string;
    name: string;
    image: string;
    status: string;
    species: string;
    description: string;

    constructor(id: string, name: string, image: string, status: string, species: string,description: string) {
        this.id = id;
        this.name = name;
        this.status = status;
        this.image = image;
        this.species = species;
        this.description = description;
    }
}