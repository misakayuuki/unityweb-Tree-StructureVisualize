mergeInto(LibraryManager.library,{   
    PostScore: function (id,dsId,type,name) { 
     strs_id = Pointer_stringify(id);
     strs_type = Pointer_stringify(type);
     strs_name = Pointer_stringify(name);
     GetScore(strs_id,dsId,strs_type,strs_name);                       
    },    
});
