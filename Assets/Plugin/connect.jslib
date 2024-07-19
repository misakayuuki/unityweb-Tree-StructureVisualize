mergeInto(LibraryManager.library,{   
    PostScore: function (sceneName) { 
     strs = Pointer_stringify(sceneName);
     GetScore(strs);                       
    },    
});
