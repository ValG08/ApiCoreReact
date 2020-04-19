
  const generateText = (name) => {
    return name;
  };
  
   const validateInput = (text) => {
    if (text == null) {
      return false;
    }
    return true;
  }; 
  
   exports.checkAndGenerate = (name) => {
    if (!validateInput(name)) {
      return false;
    }
    return generateText(name);
  }; 
  
  exports.generateText = generateText;
  exports.validateInput = validateInput;
 