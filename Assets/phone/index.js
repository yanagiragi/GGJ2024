function c() {
    alert('click');
        fetch("http://localhost:13579/HeartBeat",
        {mode:'cors'})
      .then((response) => response.json())
      .then((json) => console.log(json));

}

