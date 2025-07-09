cmake_minimum_required(VERSION 3.8)

project(NativePlugin)

# Если будет использоваться MSVC, включим поддержку EditAndContinue (по желанию)
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

# Добавляем исходники
add_library(NativePlugin SHARED
    NativePlugin.cpp
    NativePlugin.h
)

# Стандарт C++
set_target_properties(NativePlugin PROPERTIES
    CXX_STANDARD 17
    CXX_STANDARD_REQUIRED YES
)

# Windows-specific настройки
if (WIN32)
    target_compile_definitions(NativePlugin PRIVATE EXPORTING_DLL)
endif()
