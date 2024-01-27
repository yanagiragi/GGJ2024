function c() {
    alert('click');
        fetch("http://localhost:13579/HeartBeat",
        {mode:'cors'})
      .then((response) => {let json = response.json(); alert(json);return json;})
      .then((json) => alert(json));
}

