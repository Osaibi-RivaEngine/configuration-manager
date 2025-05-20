class KeyValuePair {
  Id: number;
  Key: string;
  Value: string;
  Label: string;

  constructor(id = 0, key = "", value = "") {
    this.Id = id;
    this.Key = key;
    this.Value = value;
    this.Label = `${key} = ${value}`;
  }
}

export default KeyValuePair;
