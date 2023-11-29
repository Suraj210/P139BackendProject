let datas = {
  id1: {
    name: "Sara",
    class: "V",
    subject: ["english", "math", "science"],
  },
  id2: {
    name: "David",
    class: "V",
    subject: ["english", "math", "science"],
  },
  id3: {
    name: "Sara",
    class: "V",
    subject: ["english", "math", "science"],
  },
  id4: {
    name: "Surya",
    class: "V",
    subject: ["english", "math", "science"],
  },
};

function removeDuplicates(obj) {
  // Create an empty object to store the new object
  let newObj = {};
  // Create an empty array to store the values that have been seen
  let seen = [];
  // Loop through the keys of the original object
  for (let key in obj) {
    // Get the value of the current key
    let value = obj[key];
    // Convert the value to a JSON string
    let valueStr = JSON.stringify(value);
    // Check if the value has been seen before
    if (!seen.includes(valueStr)) {
      // If not, add it to the seen array
      seen.push(valueStr);
      // And add the key-value pair to the new object
      newObj[key] = value;
    }
  }
  // Return the new object
  return newObj;
}

console.log(removeDuplicates(datas));
