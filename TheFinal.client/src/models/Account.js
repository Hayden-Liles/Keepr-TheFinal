export class Profile {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg
  }
}

export class Account extends Profile {
  constructor(data) {
    super(data)
    this.email = data.email
  }
}
